using System;
using System.Collections.Generic;

using DUBuddy.IndustryBuildings;

namespace DUBuddy.Items
{
    internal class PureIron : IItem
    {
        public TimeSpan GetBuildTime()
        {
            return TimeSpan.FromSeconds(25);
        }

        public string GetName()
        {
            return "Pure Iron";
        }

        public bool isOre()
        {
            return false;
        }

        public int GetProductionAmount()
        {
            return 45;
        }

        public Type GetIndustryBuildingType()
        {
            return typeof(RefinerM_Building);
        }

        public double GetProductionProMinute()
        {
            TimeSpan s = GetBuildTime();
            int amount = GetProductionAmount();

            return amount / s.TotalMinutes;
        }

        private Dictionary<Type, int> recipe = new Dictionary<Type, int>()
            {
                {typeof(Hematite), 65},
            };

        public Dictionary<Type, int> GetRecipe()
        {
            return recipe;
        }

        public int GetUnitMinPrice()
        {
            return 18;
        }

        public int GetUnitMaxPrice()
        {
            return 25;
        }
    }
}
