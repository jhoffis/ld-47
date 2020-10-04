using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingInfoTimberHouse : IBuildingInfo
{
    private int maxCapacity = 100;
    private int amount = 0;
    public void Give(IUnit unit)
    {
        unit.addResource(ResourceType.TIMBER, -10);
        amount += 10;
    }

    public void Take(IUnit unit)
    {
        throw new System.NotImplementedException();
    }
}
