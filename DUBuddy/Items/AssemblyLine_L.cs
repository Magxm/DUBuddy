using System;
using System.Collections.Generic;

using DUBuddy.IndustryBuildings;

namespace DUBuddy.Items
{
    class AssemblyLine_L : IItem
    {
        public TimeSpan GetBuildTime()
        {
            return TimeSpan.FromMinutes(128);
        }

        public string GetName()
        {
            return "Assembly Line L";
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
                {typeof(BasicMobilePanel_L), 1},
                {typeof(BasicPowerSystem), 125},
                {typeof(BasicScrew), 216},
                {typeof(BasicReinforcedFrame_L), 1},
            };

        public Dictionary<Type, int> GetRecipe()
        {
            return recipe;
        }

        public int GetUnitMinPrice()
        {
            return 100000;
        }

        public int GetUnitMaxPrice()
        {
            return 300000;
        }
    }
}
