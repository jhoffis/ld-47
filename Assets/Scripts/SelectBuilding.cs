using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.UI;

public class SelectBuilding : MonoBehaviour
{
    public BuildingPlace building;
    private Button button;
    
    // Start is called before the first frame update
    void Start()
    {
	    button = gameObject.GetComponent(typeof(Button)) as Button;        
        button.onClick.AddListener(PlaceBuildingOnMouse);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void PlaceBuildingOnMouse()
    {
        building.Create();
        Instantiate(building, new Vector3(0, 0, 0), Quaternion.identity);
    }

}
