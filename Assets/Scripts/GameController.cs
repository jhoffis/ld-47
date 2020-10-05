using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public PlayerController playerController;
    [FormerlySerializedAs("TreeSpawnRate")] public int SpawnRate;
    private long _nextTreeTime;

    private GameObject timer, score;
    private long endTime;
    private long startTime;
    private bool finished;

    public Random Ran = new Random();
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            startTime = Now();
            endTime = startTime + (180*1000);
            timer = GameObject.FindWithTag("Finish");
            score = GameObject.FindWithTag("FinishScore");
            SpawnRate = 7000;
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
        long now = Now();
        GenerateResources(now);
        timer.GetComponent<TMP_Text>().text = ((now - startTime) / 1000) + "/" + ((endTime - startTime) / 1000) + " sec";
        if (now >= endTime && !finished)
        {
            score.GetComponent<TMP_Text>().text = "You got " + (playerController.Resources[ResourceType.Gold] + (playerController.Resources[ResourceType.Iron] * 2)) + " in score!";
            Destroy(playerController);
            finished = true;
        }
    }

    private void GenerateResources(long now)
    {
        if (_nextTreeTime > now) return;

        _nextTreeTime = now + 1000 + Ran.Next(SpawnRate);

        string resourcePrefab = "Prefabs/";
        if (Ran.Next(100) < 20)
            resourcePrefab += "Iron";
        else
            resourcePrefab += "Tree";
        
        var tree = Instantiate (Resources.Load (resourcePrefab) as GameObject);
        // plasser på en ledig plass tilfeldig.
        int size = 32;
        tree.transform.position = new Vector3(Ran.Next(size) - size / 2, Ran.Next(size) - size / 2);
    }

    public long Now()
    {
        return DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
    }
}
