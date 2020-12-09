using System;
using System.Collections.Generic;

using DUBuddy.IndustryBuildings;

namespace DUBuddy.Items
{
    internal class Silumin : IItem
    {
        public TimeSpan GetBuildTime()
        {
            return TimeSpan.FromMinutes(3);
        }

        public string GetName()
        {
            return "Silumin";
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
                {typeof(PureAluminium), 100},
                {typeof(PureSilicon), 50}
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
            return 100;
        }
    }
}
