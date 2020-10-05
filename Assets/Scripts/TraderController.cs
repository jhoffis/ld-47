using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class TraderController : MonoBehaviour, IUnit
{
    public ResourceType resourceType;

    private float _timer;

    private Transform targetBuilding;

    private Vector2 randomTargetPos = Vector2.zero;

    private float speed;

    private bool hasInteracted = false;
    private long _interactAgain;

    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(1f, 2f);
    }

    void MoveTowards(Vector3 target)
    {
        var step = speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, target, step);
    }


    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > 150)
        {
            MoveTowards(new Vector3(50f, 0f));
            if (Mathf.Abs(transform.position.x - 50f) < Mathf.Epsilon)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if ((((int) _timer) / 10) % 3 == 0)
            {
                if (hasInteracted && _interactAgain == 0)
                {
                    _interactAgain = GameController.Instance.Now() + 2000 + GameController.Instance.Ran.Next(1000);
                }
                else if (_interactAgain != 0 && _interactAgain < GameController.Instance.Now())
                {
                    hasInteracted = false;
                    _interactAgain = 0;
                }
                // Look for buildings
                var results = new Collider2D[10];
                var size = Physics2D.OverlapCircleNonAlloc(this.transform.position, 1, results);
                if (targetBuilding == null)
                {
                    FindNearestBuilding();
                }
                else
                {
                    if (Mathf.Abs(Vector2.Distance(transform.position, targetBuilding.position)) < 1)
                    {
                        if (!hasInteracted)
                        {
                            var script = targetBuilding.GetComponent(typeof(BuildingObject)) as BuildingObject;
                            if (script != null)
                            {
                                script.Interact(this, InteractType.TAKE);
                                gameObject.GetComponent<AudioSource>().Play();
                                hasInteracted = true;
                            }
                        }
                    }
                    else
                    {
                        MoveTowards(targetBuilding.position);
                    }
                }
            }
            else
            {
                // Walk like a robot
                var step = speed * Time.deltaTime; // calculate distance to move
                if ((randomTargetPos - (Vector2) transform.position).magnitude < 0.01)
                {
                    randomTargetPos = new Vector2(Random.Range(-10f, 10f), Random.Range(-10f, 10f));
                }

                transform.position = Vector3.MoveTowards(transform.position, randomTargetPos, step);
            }
        }
    }

    private void MoveTowardsBuilding()
    {
        var step = speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, targetBuilding.position, step);
    }

    private void FindNearestBuilding()
    {
        var colliders = Physics2D.OverlapCircleAll(this.transform.position, 100);
        foreach (var resultCollider in colliders)
        {
            if (resultCollider != null && resultCollider.tag.Equals("Building"))
            {
                if (resultCollider.transform != null)
                {
                    var buildingTransform = resultCollider.transform;

                    var script = buildingTransform.GetComponent(typeof(BuildingObject)) as BuildingObject;
                    if (script != null && script.GetBuildingInfo().GetResourceType() == resourceType)
                    {
                        targetBuilding = buildingTransform;
                        return;
                    }
                }
            }
        }
    }

    public int addHealth(int amount)
    {
        throw new NotImplementedException();
    }

    public int addResource(ResourceType resourceType, int amount)
    {
        return 3;
    }
}