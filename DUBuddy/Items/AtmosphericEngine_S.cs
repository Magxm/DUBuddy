using System;
using System.Collections.Generic;

using DUBuddy.IndustryBuildings;

namespace DUBuddy.Items
{
    class AtmosphericEngine_S : IItem
    {
        public TimeSpan GetBuildTime()
        {
            return TimeSpan.FromMinutes(32);
        }

        public string GetName()
        {
            return "Atmospheric Engine S";
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
                {typeof(BasicScrew), 3},
                {typeof(BasicInjector), 5},
                {typeof(BasicCombustionChamber_S), 1},
                {typeof(BasicReinforcedFrame_S), 1},
            };

        public Dictionary<Type, int> GetRecipe()
        {
            return recipe;
        }

        public int GetUnitMinPrice()
        {
            return 2500;
        }

        public int GetUnitMaxPrice()
        {
            return 10000;
        }
    }
}
