using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// saves and controls a set of patrol points
/// </summary>
public class PatrolManager : MonoBehaviour
{

    [SerializeField] List<Transform> patrolPoints;
    // Start is called before the first frame update
    void Awake()
    {
        if (patrolPoints.Count == 0)
        {
            patrolPoints = new List<Transform>(GetComponentsInChildren<Transform>());
            patrolPoints.Remove(transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Transform GetPatrol(int patrolIndex)
    {
        if (patrolPoints.Count == 0)
        {
            return transform;
        }

        try
        {
            return patrolPoints[patrolIndex % patrolPoints.Count];

        }
        catch (IndexOutOfRangeException e)
        {
            Console.WriteLine(e);
            return patrolPoints[0];

        }
    }

    public Tuple<Transform, int> GetRandomPatrolTuple()
    {
        Tuple<Transform, int> temp = new Tuple<Transform, int>(transform,0);
        if (patrolPoints.Count == 0)
        {
            return temp;
        }

        int randomIndex = UnityEngine.Random.Range(0, patrolPoints.Count);
        temp = new Tuple<Transform, int>(GetPatrol(randomIndex), randomIndex);
        return temp;
    }

    public int GetRandomPatrolIndex()
    {
        int randomIndex = UnityEngine.Random.Range(0, patrolPoints.Count);
        return randomIndex;
    }
}
