using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuilding
{
    // Unit tar eller gir, ikke bygning som tar eller gir.
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