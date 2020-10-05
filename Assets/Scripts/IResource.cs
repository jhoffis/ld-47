using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IResource
{
    ResourceType GetType();

    int Collect(int gatherSpeed);
}
public enum ResourceType
{
    Timber, Iron, Gold
}