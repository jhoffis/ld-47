using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuildingInfo
{
    // Unit tar eller gir, ikke bygning som tar eller gir.
    void Give(IUnit unit);
    void Take(IUnit unit);
    bool IsBroken();
    string GetInfo();
    bool CanAffordBuilding();
    void BuyBuilding();
    ResourceType GetResourceType();
}
