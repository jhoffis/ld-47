using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

delegate void UIUpdater();
public class PlayerController : MonoBehaviour, IUnit
{
    private Vector2 _velocity = Vector2.zero;
    private Rigidbody2D _rb2d;
    private Animator _anim;

    public int speed;
    public int GatherSpeed = 3;
    
    public Dictionary<ResourceType, int> Resources;
    private UIUpdater _uiUpdater;

    // Start is called before the first frame update
    private void Awake()
    {
        Resources = new Dictionary<ResourceType, int>();
        _rb2d = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();

        foreach (ResourceType resourceType in Enum.GetValues(typeof(ResourceType)))
        {
            Resources.Add(resourceType, 0);
        }
        
        Resources[ResourceType.Timber] = BuildingInfoTimberHouse.BuildingCost;
    }

    public void AddUIUpdate(Action action)
    {
        _uiUpdater += new UIUpdater(action);
    }

    public void InvokeUIUpdate()
    {
        _uiUpdater.Invoke();
    }

    private void Update()
    {
        UpdateVelocity();
        _anim.SetFloat("speed", _velocity.magnitude);

        if (_rb2d.velocity.x < -Mathf.Epsilon)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (_rb2d.velocity.x > Mathf.Epsilon)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            // Find all interactable objects nearby
            var nearest = FindNearbyObjects();
            if (nearest != null)
            {
                switch (nearest.tag)
                {
                    case "Resource":
                        var script = nearest.GetComponent(typeof(IResource)) as IResource;
                        if (script == null)
                        {
                            Debug.Log("Missing script on Resource, or Resource does not implement IResource");
                            return;
                        }

                        var type = script.GetType();
                        if (!Resources.ContainsKey(type)) Resources.Add(type, 0);
                        Resources[type] += script.Collect(GatherSpeed);
                        _uiUpdater.Invoke();

                        Debug.Log("Total amount of " + type + " is: " + Resources[type]);
                        break;
                    case "Building":
                        var buildingScript = nearest.GetComponent(typeof(BuildingObject)) as BuildingObject;
                        if (buildingScript == null)
                        {
                            Debug.Log("Missing script on Building");
                            return;
                        }

                        var interactType = InteractType.GIVE;
                        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                        {
                            gameObject.GetComponents<AudioSource>()[1].Play();
                            interactType = InteractType.TAKE;
                        }
                        else
                        {
                            gameObject.GetComponents<AudioSource>()[0].Play();
                        }
                        
                        buildingScript.Interact(this, interactType);

                        break;
                }
            }
        }
    }

    private Transform FindNearbyObjects()
    {
        var results = new Collider2D[10];
        var size = Physics2D.OverlapCircleNonAlloc(this.transform.position, 1, results);
        return size == 0
            ? null
            : (from resultCollider in results
                where resultCollider != null
                let colliderTag = resultCollider.tag
                where colliderTag != null && (colliderTag.Equals("Resource") || colliderTag.Equals("Building"))
                select resultCollider.transform).FirstOrDefault();
    }

    private void UpdateVelocity()
    {
        _velocity = _rb2d.velocity;
        var deltaX = Input.GetAxis("Horizontal") * speed;
        var deltaY = Input.GetAxis("Vertical") * speed;
        var newVelocity = new Vector2(deltaX, deltaY);

        if (Mathf.Abs(newVelocity.magnitude - _velocity.magnitude) > Mathf.Epsilon)
        {
            _velocity = newVelocity;
        }

        _rb2d.velocity = _velocity;
    }

    public int addHealth(int amount)
    {
        throw new NotImplementedException();
    }

    public int addResource(ResourceType resourceType, int amount)
    {
        int res = amount;
        if (Resources[resourceType] + amount < 0)
        {
            res = -Resources[resourceType];
            Resources[resourceType] = 0;
        }
        else Resources[resourceType] += amount;
        _uiUpdater.Invoke();
        return res;
    }
}

