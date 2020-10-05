using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class BuildingMenu : MonoBehaviour
{

    private GameObject buildingMenu;

    void Awake()
    {
        buildingMenu = transform.parent.gameObject;
        buildingMenu.SetActive(false);

        var buildingButton = Resources.Load<GameObject>("Prefabs/BuildingButton");
        var buildingButtons = new GameObject[BuildingObject.BuildingNames.Length];
        
        for (int i = 0; i < buildingButtons.Length; i++)
        {
            var pos = new Vector2(transform.position.x, 200 + transform.position.y + (i * (-40))); 
            buildingButtons[i] = Instantiate(buildingButton, pos, Quaternion.identity);
            buildingButtons[i].transform.parent = transform;
            buildingButtons[i].GetComponent<SelectBuilding>().BuildingType = i;
            buildingButtons[i].GetComponentInChildren<TMP_Text>().text = BuildingObject.BuildingNames[i];
        }

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
