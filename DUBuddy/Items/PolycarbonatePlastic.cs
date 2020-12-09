using System;
using System.Collections.Generic;

using DUBuddy.IndustryBuildings;

namespace DUBuddy.Items
{
    internal class PolycarbonatePlastic : IItem
    {
        public TimeSpan GetBuildTime()
        {
            return TimeSpan.FromMinutes(3);
        }

        public string GetName()
        {
            return "Polycarbonate Plastic";
        }

        public bool isOre()
        {
            return false;
        }

        public int GetProductionAmount()
        {
            return 75;
        }

        public Type GetIndustryBuildingType()
        {
            return typeof(ChemicalM_Building);
        }

        public double GetProductionProMinute()
        {
            TimeSpan s = GetBuildTime();
            int amount = GetProductionAmount();

            return amount / s.TotalMinutes;
        }

        private Dictionary<Type, int> recipe = new Dictionary<Type, int>()
            {
                {typeof(PureCarbon), 100},
                //{typeof(PureHydrogen), 50},
            };

        public Dictionary<Type, int> GetRecipe()
        {
            return recipe;
        }

        public int GetUnitMinPrice()
        {
            return 30;
        }

        public int GetUnitMaxPrice()
        {
            return 60;
        }
    }
}
