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
    private Dictionary<ResourceType, GameObject> resources;

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
    }


    private void InitializeResourceUI() //FIXME: bugged
    {
        // Need testing / fixing

        // Fix this one
        //resources = playerController.Resources;

        //resources = new Dictionary<ResourceType, int>();


        GameController.Instance.playerController.AddUIUpdate(UpdateText);
        
        var playerResources = GameController.Instance.playerController.Resources;
        resources = new Dictionary<ResourceType, GameObject>();

        int i = 0;
        foreach (KeyValuePair<ResourceType, int> resource in playerResources)
        {
            Debug.Log(resource.Key);
            Debug.Log(resource.Value);
            var go = AddResourceGameObject(resource.Key.ToString(), resource.Value, i);
            go.transform.SetParent(panel.transform, false);
            resources.Add(resource.Key, go);
            i++;
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

        GameObject childTMP = new GameObject();

        // Make Child
        childTMP.transform.SetParent(resourceObject.transform);

        // Add TMP

        var m_textMeshProGUI = childTMP.AddComponent<TextMeshProUGUI>();
        m_textMeshProGUI.fontSize = 36;

        // Position

        var TMPRect = childTMP.GetComponent<RectTransform>();
        TMPRect.anchoredPosition = new Vector2(150, -8);

        // Set Value
        m_textMeshProGUI.text = value.ToString(); 

        // Return object
        return resourceObject;
    }

    public void UpdateText()
    {
        var enumerator = GameController.Instance.playerController.Resources.GetEnumerator();
        do {
            var text = resources[enumerator.Current.Key].GetComponentInChildren<TextMeshProUGUI>();
            text.text = enumerator.Current.Value.ToString();
        }
        while (enumerator.MoveNext()) ;
        enumerator.Dispose();
    }

}
