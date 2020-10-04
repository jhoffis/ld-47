using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BuildingMenu : MonoBehaviour
{


    private GameObject buildingMenu;

    void Awake()
    {
        buildingMenu = transform.parent.gameObject;
        buildingMenu.SetActive(false);
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
