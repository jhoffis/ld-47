using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{

    private GameObject upgradeMenuInterface;

    void Awake()
    {
        upgradeMenuInterface = transform.parent.gameObject;
        upgradeMenuInterface.SetActive(false);

        //var upgradeButton = Resources.Load<GameObject>("Prefabs/BuildingButton");
        //var upgradeButtons = new GameObject[BuildingObject.BuildingNames.Length];

        //for (int i = 0; i < buildingButtons.Length; i++)
        //{
        //    var pos = new Vector2(transform.position.x, 200 + transform.position.y + (i * (-40)));
        //    buildingButtons[i] = Instantiate(buildingButton, pos, Quaternion.identity);
        //    buildingButtons[i].transform.parent = transform;
        //    buildingButtons[i].GetComponent<SelectBuilding>().BuildingType = i;
        //    buildingButtons[i].GetComponentInChildren<TMP_Text>().text = BuildingObject.BuildingNames[i];
        //}

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

