using System;
using System.Collections.Generic;

using DUBuddy.IndustryBuildings;

namespace DUBuddy.Items
{
    internal class BasicReinforcedFrame_M : IItem
    {
        public TimeSpan GetBuildTime()
        {
            return TimeSpan.FromMinutes(6);
        }

        public string GetName()
        {
            return "Basic Reinforced Frame M";
        }

        public bool isOre()
        {
            return false;
        }

        public int GetProductionAmount()
        {
            return 1;
        }

        public double GetProductionProMinute()
        {
            TimeSpan s = GetBuildTime();
            int amount = GetProductionAmount();

            return amount / s.TotalMinutes;
        }

        public Type GetIndustryBuildingType()
        {
            return typeof(MetalworkM_Building);
        }

        private Dictionary<Type, int> recipe = new Dictionary<Type, int>()
            {
                {typeof(Steel), 74},
            };

        public Dictionary<Type, int> GetRecipe()
        {
            return recipe;
        }

        public int GetUnitMinPrice()
        {
            return 2672;
        }

        public int GetUnitMaxPrice()
        {
            return 7000;
        }
    }
}
