using System;
using System.Collections.Generic;

using DUBuddy.IndustryBuildings;

namespace DUBuddy.Items
{
    class BasicScrew : IItem
    {
        public TimeSpan GetBuildTime()
        {
            return TimeSpan.FromMinutes(3);
        }

        public string GetName()
        {
            return "Basic Screw";
        }

        public int GetProductionAmount()
        {
            return 10;
        }

        public bool isOre()
        {
            return false;
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
                {typeof(Steel), 10},
            };

        public Dictionary<Type, int> GetRecipe()
        {
            return recipe;
        }

        public int GetUnitMinPrice()
        {
            return 36;
        }

        public int GetUnitMaxPrice()
        {
            return 80;
        }
    }
}
