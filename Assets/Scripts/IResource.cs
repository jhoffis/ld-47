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
    Timber, Iron, Gold, Food, Wool
}