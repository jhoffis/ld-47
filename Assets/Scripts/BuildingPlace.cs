using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;
using Debug = UnityEngine.Debug;

public class BuildingPlace : MonoBehaviour, IInteractable
{
    private int type; // Bygning å vise og hente ut
    private bool interact;
    private SpriteRenderer renderer;

    public void Init(int type)
    {
        this.type = type;
        interact = false;
        renderer = gameObject.AddComponent<SpriteRenderer>();

        string path = "Sprites/building" + type;
        Sprite sprite = Resources.Load<Sprite>(path);
        if (sprite == null)
            throw new Exception("COULD NOT FIND SPRITE: " + path);
        renderer.sprite = sprite;
    }
    

    void Update()
    {
        if (!interact)
        {
            var mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mPos.x, mPos.y, 0);

            //Place om høyre knapp er nede
            if (Input.GetMouseButtonDown(1))
            {
                Debug.Log("place");
                TurnInteractable();
            }
        }
    }

    public void Create(int type)
    {
        var newOne = Instantiate(this, new Vector3(0, 0, 0), Quaternion.identity);
        newOne.Init(type);
        Debug.Log("Check building id: " + newOne.Equals(this));
    }

    private void TurnInteractable()
    {
        interact = true;
    }

    public void Interact()
    {
        if (!interact) return;
        throw new System.NotImplementedException();
    }
}
