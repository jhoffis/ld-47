using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector2 _velocity = Vector2.zero;
    private Rigidbody2D _rb2d;
    private Animator anim;

    public Dictionary<string, int> resources;



    // Start is called before the first frame update
    private void Start()
    {
        resources = new Dictionary<string, int>();
        _rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator> ();
    }

    private void Update()
    {
        UpdateVelocity();
        anim.SetFloat("speed", _velocity.magnitude);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Find all interactable objects nearby
            var nearest = FindNearbyObjects();
            if (nearest != null)
            {
                if (nearest.tag.Equals("Resource"))
                {
                    var script = nearest.GetComponent(typeof(IResource)) as IResource;
                    if (script == null)
                    {
                        Debug.Log("Missing script on Resource, or Resource does not implement IResource");
                        return;
                    }
                    var type = script.GetType().ToString();
                    if(!resources.ContainsKey(type)) resources.Add(type, 0);
                    resources[type] += script.Collect();
                    Debug.Log("Total amount of " + type + " is: " + resources[type]);
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
        var deltaX = Input.GetAxis("Horizontal");
        var deltaY = Input.GetAxis("Vertical");
        var newVelocity = new Vector2(deltaX, deltaY);

        if (Mathf.Abs(newVelocity.magnitude - _velocity.magnitude) > Mathf.Epsilon)
        {
            _velocity = newVelocity;
        }

        _rb2d.velocity = _velocity;
    }
}