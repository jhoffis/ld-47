using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    private Action _action;
    private GameObject button;
    private GameObject price;
    private GameObject upgrade;

    void Awake()
    {
        button = transform.GetChild(0).gameObject;
        price = transform.GetChild(1).gameObject;
        upgrade = transform.GetChild(2).gameObject;
    }

    public void SetUpgrade(Action action)
    {
        this._action = action;
        button.GetComponent<Button>().onClick.AddListener(Upgrade);
    }

    private void Upgrade()
    {
        _action.Invoke();
    }

    public void SetPrice(int price, ResourceType resourceType)
    {
        this.price.GetComponent<TMP_Text>().text = price + " " + resourceType;
    }

    public void SetTitle(string title)
    {
        upgrade.GetComponent<TMP_Text>().text = title;
    }
}
