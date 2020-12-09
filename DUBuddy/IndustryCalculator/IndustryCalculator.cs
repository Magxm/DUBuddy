using System;
using System.Collections.Generic;
using System.Linq;

using DUBuddy.IndustryBuildings;
using DUBuddy.Items;

namespace DUBuddy.IndustryCalculator
{
    public class IndustryResult
    {
        private Type _ResultType;
        public Type ResultType
        {
            get
            {
                return _ResultType;
            }
            set
            {
                _ResultType = value;
                _ResultTypeName = Statics.Items[value].GetName();
            }
        }

        private string _ResultTypeName;
        public string ResultTypeName
        {
            get
            {
                return _ResultTypeName;
            }
            set
            {
                _ResultTypeName = value;
                _ResultType = IndustryProductList.GetInstance().GetTypeFromName(value);
            }
        }

        public int MachineAmount { get; set; }
        public IndustryResult(Type r, int am)
        {
            ResultType = r;
            ResultTypeName = Statics.Items[r].GetName();
            MachineAmount = am;
        }
    }

    public class IndustryProductList : List<string>
    {
        private static IndustryProductList __Instance = null;
        public static IndustryProductList GetInstance()
        {
            if (__Instance == null)
            {
                __Instance = new IndustryProductList();
            }

            return __Instance;
        }

        public Dictionary<string, Type> ProductNameToType;

        public Type GetTypeFromName(string name)
        {
            return ProductNameToType[name];
        }

        public IndustryProductList()
        {
            ProductNameToType = new Dictionary<string, Type>();
            foreach (var itemTypeKV in Statics.Items)
            {
                string productName = itemTypeKV.Value.GetName();
                Type productType = itemTypeKV.Key;
                ProductNameToType.Add(productName, productType);
                this.Add(productName);
            }
        }
    }

    public enum IndustryTreeCalculationType
    {
        Overflow, //Will prefer doing one Building more than needed and selling overflows rather than less than needed buildings
        Underflow, //Will try to keep amount of buildings and resources needed at costs of production being slower
        Exact, //Exact
    }

    public class BuildingListEntry
    {
        public string Name { get; set; }
        public int Amount { get; set; }

        public BuildingListEntry(string name, int amount)
        {
            Name = name;
            Amount = amount;
        }
    }

    public class ItemListEntry
    {
        public string Name { get; set; }
        public double ProductionPerMinute { get; set; }
        public double NeededProductionAmount { get; set; }
        public string BuildingName { get; set; }
        public int BuildingAmount { get; set; }
        public double MinMoneyPerMinute { get; set; }
        public double MaxMoneyPerMinute { get; set; }

        public ItemListEntry(string name, double productionPerMinute, string buildingName, int buildingAmount, double minMM, double maxMM)
        {
            Name = name;
            ProductionPerMinute = productionPerMinute;
            BuildingName = buildingName;
            BuildingAmount = buildingAmount;
            MinMoneyPerMinute = minMM;
            MaxMoneyPerMinute = maxMM;
        }
    }

    public class OreListEntry
    {
        public string Name { get; set; }
        public double AmountNeededPerMinute { get; set; }

        public OreListEntry(string name, double amountNeededPerMinute)
        {
            Name = name;
            AmountNeededPerMinute = amountNeededPerMinute;
        }
    }

    public class IndustryCalculatorResult
    {
        public List<BuildingListEntry> BuildingList = new List<BuildingListEntry>();
        public List<ItemListEntry> ItemList = new List<ItemListEntry>();
        public List<OreListEntry> OreList = new List<OreListEntry>();
        public double OreAmountPerMinNeeded { get; set; }
        public double RecommendedMaxOreBuyPrice { get; set; }
        public double TechnicallyMaxSellMaxOreBuyPrice { get; set; }
        public double MinSellPriceSum { get; set; }
        public double MaxSellPriceSum { get; set; }
        public double OreMinDumpPrice { get; set; }
        public double OreMaxDumpPrice { get; set; }
        public double ProfitBuyingOreRecommendedSellingMax { get; set; }
        public IndustryCalculatorResult()
        {
            RecommendedMaxOreBuyPrice = 0;
            TechnicallyMaxSellMaxOreBuyPrice = 0;
            MinSellPriceSum = 0;
            MaxSellPriceSum = 0;
            OreMinDumpPrice = 0;
            OreMaxDumpPrice = 0;
            ProfitBuyingOreRecommendedSellingMax = 0;
            OreAmountPerMinNeeded = 0;
        }
    }
    public class IndustryCalculator
    {
        List<IndustryResult> __Results;
        public IndustryCalculator(List<IndustryResult> wantedResults)
        {
            __Results = wantedResults;
        }

        public class IndustryNode
        {
            public List<IndustryNode> Children = new List<IndustryNode>();
            public IIndustryBuilding Building;
            public int BuildingAmount = 0;
            public Dictionary<Type, int> ItemsNeeded;
            public IItem Product;
            public double ProductAmountPerMinute = 0;
            public double NeededProductionAmountPerMinute = 0;

            public IndustryNode(Type productType)
            {
                Product = Statics.Items[productType];
                ItemsNeeded = Product.GetRecipe();
                Building = Statics.Buildings[Product.GetIndustryBuildingType()];
            }
        }

        public IndustryCalculatorResult BuildIndustryTree(IndustryTreeCalculationType calculationType)
        {
            Queue<IndustryNode> queue = new Queue<IndustryNode>();
            Dictionary<Type, IndustryNode> existingNodes = new Dictionary<Type, IndustryNode>();

            List<IndustryNode> parentNodes = new List<IndustryNode>();
            foreach (var wantedResult in __Results)
            {
                IndustryNode parent;
                if (existingNodes.ContainsKey(wantedResult.ResultType))
                {
                    parent = existingNodes[wantedResult.ResultType];
                    parent.BuildingAmount += wantedResult.MachineAmount;
                }
                else
                {
                    parent = new IndustryNode(wantedResult.ResultType);
                    parent.BuildingAmount = wantedResult.MachineAmount;
                    existingNodes.Add(wantedResult.ResultType, parent);
                    queue.Enqueue(parent);
                    parentNodes.Add(parent);
                }

                parent.NeededProductionAmountPerMinute = parent.BuildingAmount * parent.Product.GetProductionProMinute();
            }


            while (queue.Count != 0)
            {
                //We update all IndustryNodes of the recipe items (creating them if not existing), update their member variables and then enque them. This way once the queue is empty, all have been updated.
                IndustryNode node = queue.Dequeue();

                if (node.Product.isOre())
                {
                    node.BuildingAmount = 1;
                    node.ProductAmountPerMinute = node.NeededProductionAmountPerMinute;
                    continue;
                }

                //First we recalculate the current buildings BuildingAmount and ProductAmountPerMinute based on NeededProductionAmountPerMinute. We also remember how much it increased so we can update the recipes accordingly.
                double productPerMinutePerBuilding = node.Product.GetProductionProMinute();
                //Updating amount of buildings needed.
                int oldBuildingAmount = node.BuildingAmount;
                switch (calculationType)
                {
                    case IndustryTreeCalculationType.Overflow:
                        node.BuildingAmount = (int)Math.Ceiling(node.NeededProductionAmountPerMinute / productPerMinutePerBuilding);
                        break;
                    case IndustryTreeCalculationType.Exact:
                        node.BuildingAmount = (int)Math.Ceiling(node.NeededProductionAmountPerMinute / productPerMinutePerBuilding);
                        break;
                    case IndustryTreeCalculationType.Underflow:
                        node.BuildingAmount = (int)Math.Max(Math.Floor(node.NeededProductionAmountPerMinute / productPerMinutePerBuilding), 1);
                        break;
                }

                //Updating Production values
                double previousProductAmountPerMinute = node.ProductAmountPerMinute;
                switch (calculationType)
                {
                    case IndustryTreeCalculationType.Overflow:
                        break;
                    case IndustryTreeCalculationType.Exact:
                        break;
                    case IndustryTreeCalculationType.Underflow:
                        break;
                }

                switch (calculationType)
                {
                    case IndustryTreeCalculationType.Overflow:
                        node.ProductAmountPerMinute = node.BuildingAmount * productPerMinutePerBuilding;
                        break;
                    case IndustryTreeCalculationType.Exact:
                        node.ProductAmountPerMinute = node.NeededProductionAmountPerMinute;
                        break;
                    case IndustryTreeCalculationType.Underflow:
                        node.ProductAmountPerMinute = node.BuildingAmount * productPerMinutePerBuilding;
                        break;
                }

                double productAmountPerMinuteIncreasedBy = node.ProductAmountPerMinute - previousProductAmountPerMinute;

                if (calculationType != IndustryTreeCalculationType.Exact && productAmountPerMinuteIncreasedBy == 0)
                {
                    continue;
                }

                //Then for every item needed for this, we calculate how many more we need now.
                foreach (var recipeItemPair in node.ItemsNeeded)
                {
                    Type itemType = recipeItemPair.Key;
                    int amountNeeded = recipeItemPair.Value;
                    IItem item = Statics.Items[itemType];

                    //Calculating how much more we need to add to this specific node.
                    double amountNeededPerMinuteIncrease = (productAmountPerMinuteIncreasedBy / node.Product.GetProductionAmount()) * amountNeeded;

                    //Finding the right node
                    IndustryNode recipeItemNode;
                    if (existingNodes.ContainsKey(itemType))
                    {
                        //We update it
                        recipeItemNode = existingNodes[itemType];
                        double oldProductionPerMinuteNeeded = recipeItemNode.NeededProductionAmountPerMinute;
                        recipeItemNode.NeededProductionAmountPerMinute += amountNeededPerMinuteIncrease;
                    }
                    else
                    {
                        //We create a new one
                        recipeItemNode = new IndustryNode(itemType);
                        recipeItemNode.NeededProductionAmountPerMinute = amountNeededPerMinuteIncrease;
                        existingNodes.Add(itemType, recipeItemNode);
                    }

                    //We enqueue the node, so it's BuildingAmount and ProductAmountPerMinute, as well as its recipe item get updated
                    queue.Enqueue(recipeItemNode);
                }
            }

            //Resetting parents needed production so you can compare Production to needed production to get sellable quantity
            foreach (var parent in parentNodes)
            {
                parent.NeededProductionAmountPerMinute = 0;
            }

            //Collect all Data
            var allNodes = existingNodes.Values.ToList();
            IndustryCalculatorResult result = new IndustryCalculatorResult();
            Dictionary<IIndustryBuilding, int> buildingAmounts = new Dictionary<IIndustryBuilding, int>();
            foreach (var node in allNodes)
            {

                if (node.Product.isOre())
                {
                    result.OreMinDumpPrice += node.Product.GetUnitMinPrice() * node.NeededProductionAmountPerMinute;
                    result.OreMaxDumpPrice += node.Product.GetUnitMaxPrice() * node.NeededProductionAmountPerMinute;
                    //Console.WriteLine("Needing " + sellableAmountPerMinute + " of " + node.Product.GetName());
                    result.OreList.Add(new OreListEntry(node.Product.GetName(), node.NeededProductionAmountPerMinute));
                }
                else
                {
                    double sellableAmountPerMinute = node.ProductAmountPerMinute - node.NeededProductionAmountPerMinute;
                    double minSellPrice = node.Product.GetUnitMinPrice() * sellableAmountPerMinute;
                    double maxSellPrice = node.Product.GetUnitMaxPrice() * sellableAmountPerMinute;
                    result.MinSellPriceSum += minSellPrice;
                    result.MaxSellPriceSum += maxSellPrice;
                    result.ItemList.Add(new ItemListEntry(node.Product.GetName(), node.NeededProductionAmountPerMinute, node.Building.GetName(), node.BuildingAmount, minSellPrice, maxSellPrice));
                }

                if (buildingAmounts.ContainsKey(node.Building))
                {
                    buildingAmounts[node.Building] += node.BuildingAmount;
                }
                else
                {
                    buildingAmounts.Add(node.Building, node.BuildingAmount);
                }
            }


            //Convert Buildings list into results BuildingList
            foreach (var buildingsNeededEntry in buildingAmounts)
            {
                string name = buildingsNeededEntry.Key.GetName();
                result.BuildingList.Add(new BuildingListEntry(name, buildingsNeededEntry.Value));
            }

            //Calculating Ore prices
            result.OreAmountPerMinNeeded = 0;
            foreach (var ore in result.OreList)
            {
                result.OreAmountPerMinNeeded += ore.AmountNeededPerMinute;
            }

            result.TechnicallyMaxSellMaxOreBuyPrice = result.MaxSellPriceSum / result.OreAmountPerMinNeeded;
            result.RecommendedMaxOreBuyPrice = ((result.MinSellPriceSum + result.MaxSellPriceSum) / 2 * 0.8) / result.OreAmountPerMinNeeded;


            result.ProfitBuyingOreRecommendedSellingMax = result.MaxSellPriceSum - (result.RecommendedMaxOreBuyPrice * result.OreAmountPerMinNeeded);
            return result;
        }
    }
}
