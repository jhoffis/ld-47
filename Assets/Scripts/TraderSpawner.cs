using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraderSpawner : MonoBehaviour
{
    public GameObject traderPrefab;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > 20)
        {
            time = 0f;
            Instantiate(traderPrefab,
                new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), Random.Range(-10f, 10f)),
                Quaternion.identity, transform);
        }
    }
}