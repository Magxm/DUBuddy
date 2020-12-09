using System;
using System.Collections.Generic;

using DUBuddy.IndustryBuildings;
namespace DUBuddy.Items
{
    internal class BasicReinforcedFrame_S
    {
        public TimeSpan GetBuildTime()
        {
            return TimeSpan.FromMinutes(2);
        }

        public string GetName()
        {
            return "Basic Reinforced Frame S";
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
                {typeof(Steel), 11},
            };

        public Dictionary<Type, int> GetRecipe()
        {
            return recipe;
        }

        public int GetUnitMinPrice()
        {
            return 397;
        }

        public int GetUnitMaxPrice()
        {
            return 1100;
        }
    }
}
