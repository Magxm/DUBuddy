using System;
using System.Collections.Generic;

using DUBuddy.IndustryBuildings;

namespace DUBuddy.Items
{
    internal class BasicBurner : IItem
    {
        public TimeSpan GetBuildTime()
        {
            return TimeSpan.FromMinutes(1);
        }

        public string GetName()
        {
            return "Basic Burner";
        }

        public bool isOre()
        {
            return false;
        }

        public int GetProductionAmount()
        {
            return 1;
        }

        public Type GetIndustryBuildingType()
        {
            return typeof(MetalworkM_Building);
        }

        public double GetProductionProMinute()
        {
            TimeSpan s = GetBuildTime();
            int amount = GetProductionAmount();

            return amount / s.TotalMinutes;
        }

        private Dictionary<Type, int> recipe = new Dictionary<Type, int>()
            {
                {typeof(BasicScrew), 4},
                {typeof(Silumin), 6},
            };

        public Dictionary<Type, int> GetRecipe()
        {
            return recipe;
        }

        public int GetUnitMinPrice()
        {
            return 361;
        }

        public int GetUnitMaxPrice()
        {
            return 750;
        }
    }
}
