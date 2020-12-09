using System;
using System.Collections.Generic;
using System.Linq;

using DUBuddy.IndustryBuildings;
using DUBuddy.Items;

namespace DUBuddy
{
    static class Statics
    {
        public static Dictionary<Type, IItem> Items = new Dictionary<Type, IItem>();
        public static Dictionary<Type, IIndustryBuilding> Buildings = new Dictionary<Type, IIndustryBuilding>();
        public static void Init()
        {
            var itemTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => typeof(IItem).IsAssignableFrom(p) && !p.IsInterface);

            foreach (var itemType in itemTypes)
            {
                IItem instance = (IItem)Activator.CreateInstance(itemType);
                Items.Add(itemType, instance);
            }


            var buildingTypes = AppDomain.CurrentDomain.GetAssemblies()
    .SelectMany(s => s.GetTypes())
    .Where(p => typeof(IIndustryBuilding).IsAssignableFrom(p) && !p.IsInterface);

            foreach (var buildingType in buildingTypes)
            {
                IIndustryBuilding instance = (IIndustryBuilding)Activator.CreateInstance(buildingType);
                Buildings.Add(buildingType, instance);
            }
        }
    }
}
