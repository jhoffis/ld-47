using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingInfoTimberHouse : IBuildingInfo
{
    public static readonly int BuildingCost = 10;
    
    private int _maxCapacity = 20;
    private int _amountResource = 0;
    private int _gold;
    private int _transferSpeed = 2;
    
    public void Give(IUnit unit)
    {
        if (_amountResource > _maxCapacity) return;
        
        _amountResource += -unit.addResource(ResourceType.Timber, -_transferSpeed);
        Debug.Log(_amountResource);
    }

    public void Take(IUnit unit)
    {
        if (unit.GetType() == typeof(TraderController))
        {
            // Du må ha noe å selge
            if (_amountResource <= 0) return;
            
            // Handleren må ha gull å gi.
            int gives = unit.addResource(ResourceType.Gold, -_transferSpeed);
            if (gives == 0) return;
            else if (_amountResource - gives < 0)
            {
                // Handleren gav for mye gull
                unit.addResource(ResourceType.Gold, _amountResource - gives);
                gives = _amountResource; 
            }
            
            // Selg
            unit.addResource(ResourceType.Timber, gives);
            _amountResource -= gives;
            _gold += gives;
            
            // Ødelegg (SKAL DETTE BEHOLDES??)
            _maxCapacity -= gives;
        }
        else
        {
            // Give Gold
            _gold -= unit.addResource(ResourceType.Gold, _gold);
        }
        
        Debug.Log(_amountResource + ", " + _gold);
    }

    public bool IsBroken()
    {
        return _maxCapacity <= 0;
    }

    public string GetInfo()
    {
        return _amountResource + "/" + _maxCapacity + "\n" + _gold + "GOLD";
    }

    public bool CanAffordBuilding()
    {
        return GameController.Instance.playerController.Resources[ResourceType.Timber] >= BuildingCost;
    }

    public void BuyBuilding()
    {
        GameController.Instance.playerController.Resources[ResourceType.Timber] -= BuildingCost;
    }

    public ResourceType GetResourceType()
    {
        return ResourceType.Timber;
    }
}
