﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Kan være for eksempel traders, player osv.
public interface IUnit
{
    int addHealth(int amount);
    int addResource(ResourceType resourceType, int amount); // add or remove
}
