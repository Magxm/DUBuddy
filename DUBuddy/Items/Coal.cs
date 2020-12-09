using System;
using System.Collections.Generic;

using DUBuddy.IndustryBuildings;

namespace DUBuddy.Items
{
    internal class Coal : IItem
    {
        public TimeSpan GetBuildTime()
        {
            return TimeSpan.FromMinutes(1);
        }

        public string GetName()
        {
            return "Coal (Ore)";
        }

        public bool isOre()
        {
            return true;
        }

        public int GetProductionAmount()
        {
            return 1;
        }

        public double GetProductionProMinute()
        {
            TimeSpan s = GetBuildTime();
            int amount = GetProductionAmount();

            return amount / s.TotalMinutes;
        }


        public Type GetIndustryBuildingType()
        {
            return typeof(OreProvider);
        }

        private Dictionary<Type, int> recipe = new Dictionary<Type, int>();

        public Dictionary<Type, int> GetRecipe()
        {
            return recipe;
        }

        public int GetUnitMinPrice()
        {
            return 12;
        }

        public int GetUnitMaxPrice()
        {
            return 18;
        }
    }
}
