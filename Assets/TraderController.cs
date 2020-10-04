using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class TraderController : MonoBehaviour
{
    public BuildingType buildingType;

    private float _timer;

    private Transform targetBuilding;

    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(2f, 5f);
    }


    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if ((((int) _timer) / 10) % 10 == 0)
        {
            // Look for buildings
            var results = new Collider2D[10];
            var size = Physics2D.OverlapCircleNonAlloc(this.transform.position, 1, results);
            if (targetBuilding == null)
            {
                FindNearestBuilding();
            }
            else
            {
                MoveTowardsBuilding();
            }
        }
        else
        {
            // Walk like a robot
            var step = speed * Time.deltaTime; // calculate distance to move
            var target = new Vector2(Random.Range(-10f, 10f), Random.Range(-10f, 10f));
            transform.position = Vector3.MoveTowards(transform.position, target, step);
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

                    var script = buildingTransform.GetComponent(typeof(BuildingPlace)) as BuildingPlace;
                    if (script != null && script.GetBuildingInfo().GetBuildingType() == buildingType)
                    {
                        targetBuilding = buildingTransform;
                    }
                }
            }
        }
    }
}