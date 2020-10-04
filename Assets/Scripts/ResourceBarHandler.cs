using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceBarHandler : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject panel;


    void Awake()
    {
        panel = transform.Find("ResourcePnl").gameObject;
    }

    void Start()
    {
        InitializeResourceUI();
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: testing
        if(Input.GetKey(KeyCode.Escape))
        {
            AddOrRemoveResourceValueUI(ResourceType.MONEY, 10);
        }
    }


    private void InitializeResourceUI() //FIXME: bugged
    {
        // Need testing / fixing

        // Fix this one
        //resources = playerController.Resources;

        //resources = new Dictionary<ResourceType, int>();



        int i = 0;
        foreach (KeyValuePair<ResourceType, int> resource in GameController.Instance.playerController.Resources)
        {
            Debug.Log(resource.Key);
            Debug.Log(resource.Value);
            AddResourceGameObject(resource.Key.ToString(), resource.Value, i++).transform.SetParent(panel.transform, false);
        }
    }

    private GameObject AddResourceGameObject(string key, int value, int objectNumber)
    {
        // Create Resource Object
        GameObject resourceObject = new GameObject(key);

        // Set Layer
        //resourceObject.layer = layer

        // Add Rect Transform

        RectTransform resourceObjectRect = resourceObject.AddComponent<RectTransform>();

        // Change position

        resourceObjectRect.anchoredPosition = new Vector2(50f + 200f * objectNumber, 0);
        resourceObjectRect.anchorMin = new Vector2(0, 0.5f);
        resourceObjectRect.anchorMax = new Vector2(0, 0.5f);
        resourceObjectRect.sizeDelta = new Vector2(200, 100);

        // Add Image Object

        GameObject imageObject = new GameObject(key + "Image");
        // Make Child 

        imageObject.transform.SetParent(resourceObject.transform);

        Image image = imageObject.AddComponent<Image>();
        String spriteLocation = "Sprites/" + key;
        image.sprite = Resources.Load<Sprite>(spriteLocation);
        RectTransform imageRect = image.GetComponent<RectTransform>();
        imageRect.anchoredPosition = new Vector2(0, 0);
        resourceObjectRect.sizeDelta = new Vector2(100, 100);

        // Add Text Object

        GameObject TMP = new GameObject();

        // Make Child
        TMP.transform.SetParent(resourceObject.transform);

        // Add TMP

        TextMeshProUGUI m_textMeshProGUI = TMP.AddComponent<TextMeshProUGUI>();
        m_textMeshProGUI.fontSize = 36;

        // Position

        RectTransform TMPRect = TMP.GetComponent<RectTransform>();
        TMPRect.anchoredPosition = new Vector2(150, -8);

        // Set Value
        m_textMeshProGUI.text = value.ToString(); 

        // Return object
        return resourceObject;
    }

    public void AddOrRemoveResourceValueUI(ResourceType type, int value) //FIXME: Bugged
    {
        String name = type.ToString();
        GameObject obj = GetResourceObjectByName(name);

        int current = int.Parse(obj.GetComponent<TextMeshProUGUI>().text.ToString());
        int total = current + value;
        //Debug.Log(obj.GetComponent<TextMeshProUGUI>().text.ToString());
        obj.GetComponent<TextMeshProUGUI>().text = total.ToString();
    }

    private GameObject GetResourceObjectByName(String name)
    {
        return GameObject.Find(name);
    }
}
