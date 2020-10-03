using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IResource
{
    ResourceType GetType();

    int Collect();
}

public enum ResourceType
{
    TIMBER, IRON, MONEY
}
