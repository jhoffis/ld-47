using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.WSA.Input;
using Debug = UnityEngine.Debug;

public class BuildingPlace : MonoBehaviour, IInteractable
{
    public static readonly string[] BuildingNames = { "Timber House" };

    private int type; // Bygning å vise og hente ut
    private bool interact;
    private SpriteRenderer renderer;
    private IBuilding _building;
    private Color originalColor;

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
        
        Color tmp = gameObject.GetComponent<SpriteRenderer>().color;
        originalColor = new Color(tmp.r, tmp.g, tmp.b, tmp.a);
        ChangeColor(1f, 1f, 1f, 0.6f);

        gameObject.layer = 5;
        renderer.sortingOrder = 2;
    }
    

    void Update()
    {
        if (!interact)
        {
            var mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mPos.x = (float) Math.Round(mPos.x);
            mPos.y = (float) Math.Round(mPos.y);
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

    public IBuilding GetBuildingInfo()
    {
        return _building;
    }

    public void Interact(IUnit unit, InteractType interactType)
    {
        if (!interact) return;
        switch (interactType)
        {
            case InteractType.GIVE:
                Debug.Log("gi meg ting");
                _building.Give(unit);
                break;
            case InteractType.TAKE:
                Debug.Log("ta ting");
                _building.Take(unit);
                break;
            default:
                break;
        }
    }

    public string GetName()
    {
        return BuildingNames[type];
    }

    public string GetNameWithoutWhitespace()
    {
        return new string(GetName().ToCharArray()
            .Where(c => !Char.IsWhiteSpace(c))
            .ToArray());
    }

    private void TurnInteractable()
    {
        interact = true;
        gameObject.layer = 0;
        renderer.sortingOrder = 0;
        ChangeColor(1f, 1f, 1f, 1f);

        var boxCollider = gameObject.AddComponent<BoxCollider2D>();
        float playersHeight = 0.45f; //FIXME hent ut skikkelig verdi. Dette er slik at spilleren kan "gå helt opp til bygningen"
        boxCollider.size = new Vector2(1f,1f - playersHeight);
        boxCollider.offset = new Vector2(0, playersHeight / 2.3f);    

        var typeClass = Type.GetType("Building" + GetNameWithoutWhitespace());
        if (typeClass == null)
            throw new Exception("COULD NOT FIND CLASS");
        
        _building = Activator.CreateInstance(typeClass, false) as IBuilding;
    }

    private void ChangeColor(float r, float g, float b, float a)
    {
        Color tmp = new Color(originalColor.r, originalColor.g, originalColor.b, originalColor.a);
        tmp.r *= r;
        tmp.g *= g;
        tmp.b *= b;
        tmp.a *= a;
        gameObject.GetComponent<SpriteRenderer>().color = tmp;
    }

}
