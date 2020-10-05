using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    private PlayerController playerController;
    private GameObject button;
    private GameObject price;
    private GameObject upgrade;

    void Awake()
    {
        playerController = GameController.Instance.playerController;
        button = transform.GetChild(0).gameObject;
        price = transform.GetChild(1).gameObject;
        upgrade = transform.GetChild(2).gameObject;
    }

    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
