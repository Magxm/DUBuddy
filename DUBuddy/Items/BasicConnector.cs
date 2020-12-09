using System;
using System.Collections.Generic;

using DUBuddy.IndustryBuildings;

namespace DUBuddy.Items
{
    internal class BasicConnector : IItem
    {
        public TimeSpan GetBuildTime()
        {
            return TimeSpan.FromMinutes(2);
        }

        public string GetName()
        {
            return "Basic Connector";
        }

        public int GetProductionAmount()
        {
            return 10;
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
            return typeof(ElectronicsIndustryM_Building);
        }

        private Dictionary<Type, int> recipe = new Dictionary<Type, int>()
            {
                {typeof(AlFeAlloy), 10},
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
            return 75;
        }
    }
}
