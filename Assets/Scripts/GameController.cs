using System;
using UnityEngine;
using Random = System.Random;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public PlayerController playerController;

    private long _nextTreeTime;
    private Random ran = new Random();
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
        else
        {
            // I am somehow the instance
        }
    }

    void Update()
    {
        GenerateResources();
    }

    private void GenerateResources()
    {
        long now = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        if (_nextTreeTime > now) return;

        _nextTreeTime = now + 5000 + ran.Next(5000);
        
        var tree = Instantiate (Resources.Load ("Prefabs/Tree") as GameObject);
        // plasser på en ledig plass tilfeldig.
        int size = 32;
        tree.transform.position = new Vector3(ran.Next(size) - size / 2, ran.Next(size) - size / 2);
        Debug.Log("Tree spawned at " + tree.transform.position.ToString());
    }
}