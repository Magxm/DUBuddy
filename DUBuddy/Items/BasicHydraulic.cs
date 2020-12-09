using System;
using System.Collections.Generic;

using DUBuddy.IndustryBuildings;

namespace DUBuddy.Items
{
    internal class BasicHydraulic : IItem
    {
        public TimeSpan GetBuildTime()
        {
            return TimeSpan.FromMinutes(1);
        }

        public string GetName()
        {
            return "Basic Hydraulic";
        }

        public int GetProductionAmount()
        {
            return 1;
        }

        public bool isOre()
        {
            return false;
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
                {typeof(Steel), 6},
                {typeof(BasicPipe), 4}
            };

        public Dictionary<Type, int> GetRecipe()
        {
            return recipe;
        }

        public int GetUnitMinPrice()
        {
            return 400;
        }

        public int GetUnitMaxPrice()
        {
            return 900;
        }
    }
}
