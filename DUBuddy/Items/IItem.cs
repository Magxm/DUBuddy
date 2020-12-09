using System;
using System.Collections.Generic;

namespace DUBuddy.Items
{
    public interface IItem
    {
        Dictionary<Type, int> GetRecipe();
        int GetProductionAmount();
        double GetProductionProMinute();
        bool isOre();
        TimeSpan GetBuildTime();
        string GetName();
        Type GetIndustryBuildingType();
        int GetUnitMinPrice();
        int GetUnitMaxPrice();
    }
}
