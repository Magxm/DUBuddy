using System;
using System.Collections.Generic;

using DUBuddy.IndustryBuildings;
namespace DUBuddy.Items
{
    class Printer3D_M : IItem
    {
        public TimeSpan GetBuildTime()
        {
            return TimeSpan.FromMinutes(32);
        }

        public string GetName()
        {
            return "3DPrinter M";
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
                {typeof(BasicInjector), 25},
                {typeof(BasicPipe), 36},
                {typeof(BasicReinforcedFrame_M), 1},
            };

        public Dictionary<Type, int> GetRecipe()
        {
            return recipe;
        }

        public int GetUnitMinPrice()
        {
            return 14000;
        }

        public int GetUnitMaxPrice()
        {
            return 50000;
        }
    }
}
