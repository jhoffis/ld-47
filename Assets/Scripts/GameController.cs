using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public PlayerController playerController;

    private long _nextTreeTime;

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

        _nextTreeTime = now + 1000;
        
        Instantiate (Resources.Load ("Prefabs/Tree") as GameObject);
        // plasser på en ledig plass tilfeldig.
        
    }
}
