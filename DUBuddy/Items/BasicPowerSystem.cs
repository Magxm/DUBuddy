using System;
using System.Collections.Generic;

using DUBuddy.IndustryBuildings;

namespace DUBuddy.Items
{
    class BasicPowerSystem : IItem
    {
        public TimeSpan GetBuildTime()
        {
            return TimeSpan.FromMinutes(1);
        }

        public string GetName()
        {
            return "Basic Power System";
        }

        public int GetProductionAmount()
        {
            return 1;
        }

        public bool isOre()
        {
            return false;
        }

        public Type GetIndustryBuildingType()
        {
            return typeof(ElectronicsIndustryM_Building);
        }

        public double GetProductionProMinute()
        {
            TimeSpan s = GetBuildTime();
            int amount = GetProductionAmount();

            return amount / s.TotalMinutes;
        }

        private Dictionary<Type, int> recipe = new Dictionary<Type, int>()
            {
                {typeof(BasicConnector), 4},
                {typeof(AlFeAlloy), 6 },
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
            return 1500;
        }
    }
}
