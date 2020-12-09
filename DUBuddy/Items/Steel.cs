using System;
using System.Collections.Generic;

using DUBuddy.IndustryBuildings;

namespace DUBuddy.Items
{
    class Steel : IItem
    {
        public TimeSpan GetBuildTime()
        {
            return TimeSpan.FromMinutes(3);
        }

        public string GetName()
        {
            return "Steel";
        }

        public int GetProductionAmount()
        {
            return 75;
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
            return typeof(SmelterM_Building);
        }

        private Dictionary<Type, int> recipe = new Dictionary<Type, int>()
            {
                {typeof(PureIron), 100},
                {typeof(PureCarbon), 50}
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
            return 180;
        }
    }
}
