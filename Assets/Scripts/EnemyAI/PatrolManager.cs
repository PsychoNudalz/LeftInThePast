using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        return patrolPoints[patrolIndex % patrolPoints.Count];
    }
}
