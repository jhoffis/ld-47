using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iron : MonoBehaviour, IResource
{
    public ResourceType GetType()
    {
        return ResourceType.Iron;
    }

    public int Collect(int gatherSpeed)
    {
        gameObject.GetComponent<AudioSource>().Play();
        Destroy(gameObject.GetComponent<SpriteRenderer>());
        Destroy(gameObject.GetComponent<BoxCollider2D>());
        return Random.Range(1, 3);
    }
}
