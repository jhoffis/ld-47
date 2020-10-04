using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingTimberHouse : IBuilding
{
    private int maxCapacity = 100;
    private int amount = 0;
    public void Give(IUnit unit)
    {
        throw new System.NotImplementedException();
    }

    public void Take(IUnit unit)
    {
        throw new System.NotImplementedException();
    }
}
