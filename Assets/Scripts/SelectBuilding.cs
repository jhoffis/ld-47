using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.UI;

public class SelectBuilding : MonoBehaviour
{
    public int BuildingType;
    private BuildingObject _buildingReference, _buildingObjectToPlace;
    private Button _button;

    // Start is called before the first frame update
    void Start()
    {
        _buildingReference = Resources.Load<BuildingObject>("Prefabs/BuildingReferencePoint");
	    _button = gameObject.GetComponent(typeof(Button)) as Button;        
        _button.onClick.AddListener(PlaceBuildingOnMouse);
    }

    void PlaceBuildingOnMouse()
    {
        if (_buildingObjectToPlace != null && _buildingObjectToPlace.IsInteractable() == false)
            Destroy(_buildingObjectToPlace);
        _buildingObjectToPlace = _buildingReference.Create(BuildingType); // FIXME add more buildings than just one
    }

}
