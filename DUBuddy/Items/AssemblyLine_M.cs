using System;
using System.Collections.Generic;

using DUBuddy.IndustryBuildings;

namespace DUBuddy.Items
{
    class AssemblyLine_M : IItem
    {
        public TimeSpan GetBuildTime()
        {
            return TimeSpan.FromMinutes(32);
        }

        public string GetName()
        {
            return "Assembly Line M";
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
                {typeof(BasicScrew), 36},
                {typeof(BasicPowerSystem), 25},
                {typeof(BasicMobilePanel_M), 1},
                {typeof(BasicReinforcedFrame_M), 1},
            };

        public Dictionary<Type, int> GetRecipe()
        {
            return recipe;
        }

        public int GetUnitMinPrice()
        {
            return 15672;
        }

        public int GetUnitMaxPrice()
        {
            return 40000;
        }
    }
}
