using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottomMenu : MonoBehaviour
{
    private GameObject bottomMenu;
    public GameObject buildingMenu;
    Button[] buttons;
    void Start()
    {
        bottomMenu = GetComponent<GameObject>();
        //buttons = GetComponentsInChildren<Button>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    
    public void switchMenuActive(GameObject menu)
    {
        menu.SetActive(!(menu.activeSelf));
    }

}
