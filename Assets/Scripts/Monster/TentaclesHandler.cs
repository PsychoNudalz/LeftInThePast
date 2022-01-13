using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = System.Random;

/// <summary>
/// Controls all tentacles on the monster
/// shoots tentacles at a fixed random rate
/// </summary>
public class TentaclesHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject originalTentacle;


    [SerializeField]
    private List<string> playerTagList;

    [SerializeField]
    private List<string> itemTagList;
    
    
    [SerializeField]
    private List<string> cloneTagList;

    [Header("Spawn Pool")]
    [SerializeField]
    private Tentacle[] tentaclesPool = Array.Empty<Tentacle>();

    [SerializeField]
    private int size = 10;

    [SerializeField]
    private List<Tentacle> freeTentacleStack = new List<Tentacle>();

    [SerializeField]
    private int index = 0;

    [Header("Spawning Settings")]
    [SerializeField]
    private Vector3 spawnArea;

    [SerializeField]
    float spawnFrequency = 0.5f;


    private float lastShootTentacleTime = 0;

    public List<string> PlayerTagList => playerTagList;
    public List<string> ItemTagList => itemTagList;

    public List<string> CloneTagList => cloneTagList;

    [ContextMenu("Awake")]
    private void Awake()
    {
        for (int i = 1; i < size; i++)
        {
            Instantiate(originalTentacle, transform);
        }

        if (tentaclesPool.Length == 0)
        {
            tentaclesPool = GetComponentsInChildren<Tentacle>();
        }

        foreach (Tentacle tentacle in tentaclesPool)
        {
            tentacle.TentaclesHandler = this;
            AddToFreeStack(tentacle);
        }
        
    }

    private void Update()
    {
        if (Time.time >= lastShootTentacleTime + spawnFrequency)
        {
            lastShootTentacleTime = Time.time;
            if (freeTentacleStack.Count > 0)
            {
                ShootFreeTentacle();
            }
        }
    }

    /// <summary>
    /// retracted tentacles will be added to a stack to signify they are free to shoot
    /// this methods shoots the tentacle at the top of the stack
    /// </summary>
    private void ShootFreeTentacle()
    {
        Tentacle tentacle = freeTentacleStack[0];
        tentacle.transform.position = transform.position+ new Vector3(UnityEngine.Random.Range(-spawnArea.x, spawnArea.x),
            UnityEngine.Random.Range(-spawnArea.y, spawnArea.y), UnityEngine.Random.Range(-spawnArea.z, spawnArea.z));
        tentacle.transform.forward = (new Vector3(UnityEngine.Random.Range(-1f, 1f),
            UnityEngine.Random.Range(-1, 1), UnityEngine.Random.Range(-1, 1))).normalized;
        tentacle.ShootTentacle();
        freeTentacleStack.RemoveAt(0);
    }

    public void AddToFreeStack(Tentacle t)
    {
        if (!freeTentacleStack.Contains(t))
        {
            freeTentacleStack.Add(t);
        }
    }

    public void RecallAllTentacles()
    {
        foreach (Tentacle tentacle in tentaclesPool)
        {
            tentacle.RetractTentacle();
        }
    }

    public void ShootAllTentacles()
    {
        RecallAllTentacles();
        foreach (Tentacle tentacle in tentaclesPool)
        {
            tentacle.ShootTentacle();
        }
    }
}