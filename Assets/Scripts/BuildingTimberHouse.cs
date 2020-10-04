using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingTimberHouse : IBuildingInfo
{
    private int maxCapacity = 100;
    private int _amount = 0;
    public void Give(IUnit unit)
    {
        unit.addResource(ResourceType.TIMBER, -10);
        _amount += 10;
        Debug.Log(_amount);
    }

    public void Take(IUnit unit)
    {
        throw new System.NotImplementedException();
    }
    public BuildingType GetBuildingType()
    {
        return BuildingType.Timber;
    }
}