using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingInfoTimberHouse : IBuildingInfo
{
    private int maxCapacity = 100;
    private int amount = 0;
    public void Give(IUnit unit)
    {
        int gives = 1;
        if (amount + gives > maxCapacity)
            gives = maxCapacity - amount;
        Debug.Log(unit.addResource(ResourceType.TIMBER, -gives));
        amount += gives;
    }

    public void Take(IUnit unit)
    {
        int takes = 1;
        if (amount - takes < 0)
            takes = amount;
        Debug.Log(unit.addResource(ResourceType.TIMBER, takes));
        amount -= takes;
    }

}
