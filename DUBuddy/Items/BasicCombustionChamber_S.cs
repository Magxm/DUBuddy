using System;
using System.Collections.Generic;

using DUBuddy.IndustryBuildings;

namespace DUBuddy.Items
{
    internal class BasicCombustionChamber_S : IItem
    {
        public TimeSpan GetBuildTime()
        {
            return TimeSpan.FromMinutes(3);
        }

        public string GetName()
        {
            return "Basic Combustion Chamber S";
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
                {typeof(BasicPipe), 6},
                {typeof(Steel), 7 },
            };

        public Dictionary<Type, int> GetRecipe()
        {
            return recipe;
        }

        public int GetUnitMinPrice()
        {
            return 433;
        }

        public int GetUnitMaxPrice()
        {
            return 1800;
        }
    }
}
