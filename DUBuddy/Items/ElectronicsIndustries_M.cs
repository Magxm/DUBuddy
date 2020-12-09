using System;
using System.Collections.Generic;

using DUBuddy.IndustryBuildings;

namespace DUBuddy.Items
{
    class ElectronicsIndustries_M : IItem
    {
        public TimeSpan GetBuildTime()
        {
            return TimeSpan.FromMinutes(32);
        }

        public string GetName()
        {
            return "Electronics Industries M";
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
                {typeof(BasicRobotArm_M), 1},
                {typeof(BasicElectronics), 25},
                {typeof(BasicPipe), 36},
                {typeof(BasicReinforcedFrame_M), 1},
            };

        public Dictionary<Type, int> GetRecipe()
        {
            return recipe;
        }

        public int GetUnitMinPrice()
        {
            return 30000;
        }

        public int GetUnitMaxPrice()
        {
            return 50000;
        }
    }
}
