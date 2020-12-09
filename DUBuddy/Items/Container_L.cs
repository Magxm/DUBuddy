using System;
using System.Collections.Generic;

using DUBuddy.IndustryBuildings;

namespace DUBuddy.Items
{
    class Container_L : IItem
    {
        public TimeSpan GetBuildTime()
        {
            return TimeSpan.FromMinutes(96);
        }

        public string GetName()
        {
            return "Container L";
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
            return typeof(AssemblyLineL_Building);
        }

        public double GetProductionProMinute()
        {
            TimeSpan s = GetBuildTime();
            int amount = GetProductionAmount();

            return amount / s.TotalMinutes;
        }

        private Dictionary<Type, int> recipe = new Dictionary<Type, int>()
            {
                {typeof(BasicHydraulic), 250},
                {typeof(BasicComponent), 432},
                {typeof(BasicReinforcedFrame_L), 2},
            };

        public Dictionary<Type, int> GetRecipe()
        {
            return recipe;
        }

        public int GetUnitMinPrice()
        {
            return 150000;
        }

        public int GetUnitMaxPrice()
        {
            return 275000;
        }
    }
}
