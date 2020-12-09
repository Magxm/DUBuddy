using System;
using System.Collections.Generic;

using DUBuddy.IndustryBuildings;


namespace DUBuddy.Items
{
    class ItemTemplate : IItem
    {
        public TimeSpan GetBuildTime()
        {
            return TimeSpan.FromMinutes(1);
        }

        public string GetName()
        {
            return "Item Template";
        }

        public bool isOre()
        {
            return false;
        }

        public int GetProductionAmount()
        {
            return 100;
        }

        public Type GetIndustryBuildingType()
        {
            return typeof(AssemblyLineM_Building);
        }

        public double GetProductionProMinute()
        {
            TimeSpan s = GetBuildTime();
            int amount = GetProductionAmount();

            return amount / s.TotalMinutes;
        }

        private Dictionary<Type, int> recipe = new Dictionary<Type, int>()
            {
                {typeof(ItemTemplate), 5},
            };

        public Dictionary<Type, int> GetRecipe()
        {
            return recipe;
        }

        public int GetUnitMinPrice()
        {
            return 100;
        }

        public int GetUnitMaxPrice()
        {
            return 200;
        }
    }
}
