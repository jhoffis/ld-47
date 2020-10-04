using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuilding
{
    void Give(IUnit unit);
    void Take(IUnit unit);
}
