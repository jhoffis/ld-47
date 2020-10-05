using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.WSA.Input;
using Debug = UnityEngine.Debug;

public class BuildingObject : MonoBehaviour, IInteractable
{
    public static readonly string[] BuildingNames = { "Timber House" };

    private int type; // Bygning å vise og hente ut
    private bool interact;
    private SpriteRenderer renderer;
    private IBuildingInfo _buildingInfo;
    private Color originalColor;

    public void Init(int type)
    {
        this.type = type;
        interact = false;
        renderer = gameObject.GetComponent<SpriteRenderer>();

        string path = "Sprites/building" + type;
        Sprite sprite = Resources.Load<Sprite>(path);
        if (sprite == null)
            throw new Exception("COULD NOT FIND SPRITE: " + path);
        renderer.sprite = sprite;
        
        Color tmp = renderer.color;
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
            } else if (Input.GetKey(KeyCode.Escape))
            {
                Destroy(gameObject);
            }
        }
    }

    public BuildingObject Create(int type)
    {
        var newOne = Instantiate(this, new Vector3(0, 0, 0), Quaternion.identity);
        newOne.Init(type);
        return newOne;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public IBuildingInfo GetBuildingInfo()
    {
        return _buildingInfo;
    }

    public void Interact(IUnit unit, InteractType interactType)
    {
        if (!interact) return;
        switch (interactType)
        {
            case InteractType.GIVE:
                _buildingInfo.Give(unit);
                break;
            case InteractType.TAKE:
                _buildingInfo.Take(unit);
                if (_buildingInfo.IsBroken())
                    Destroy(gameObject);
                break;
            default:
                break;
        }
        UpdateText();
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

    public bool IsInteractable()
    {
        return interact;
    }
    
    private void TurnInteractable()
    {
        var typeClass = Type.GetType("BuildingInfo" + GetNameWithoutWhitespace());
        if (typeClass == null)
            throw new Exception("COULD NOT FIND CLASS");
        
        _buildingInfo = Activator.CreateInstance(typeClass, false) as IBuildingInfo;

        if (!_buildingInfo.CanAffordBuilding())
        {
            _buildingInfo = null;
            return;
        }
        else
        {
            _buildingInfo.BuyBuilding();
            GameController.Instance.playerController.InvokeUIUpdate();
        }
        
        interact = true;
        gameObject.layer = 0;
        renderer.sortingOrder = 0;
        gameObject.tag = "Building";
        ChangeColor(1f, 1f, 1f, 1f);

        var boxCollider = gameObject.AddComponent<BoxCollider2D>();
        float playersHeight = 0.45f; //FIXME hent ut skikkelig verdi. Dette er slik at spilleren kan "gå helt opp til bygningen"
        boxCollider.size = new Vector2(1f,1f - playersHeight);
        boxCollider.offset = new Vector2(0, playersHeight / 2.3f);    

        gameObject.GetComponent<AudioSource>().Play();
        gameObject.SetActive(gameObject.transform.GetChild(0));
        UpdateText();
    }

    private void UpdateText()
    {
        gameObject.GetComponentInChildren<TMP_Text>().text = _buildingInfo.GetInfo();
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
