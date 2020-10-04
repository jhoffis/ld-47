using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuilding
{
    void Give(IUnit unit);
    void Take(IUnit unit);
    BuildingType GetBuildingType();
}

public enum BuildingType
{
    Timber,
    Iron,
    Gold,
    Food,
    Wool
}