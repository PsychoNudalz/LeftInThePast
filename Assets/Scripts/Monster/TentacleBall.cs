using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleBall : MonoBehaviour
{
    [SerializeField]
    private Tentacle masterTentacle;
    [SerializeField]
    private List<string> tagList;

    [SerializeField]
    private Rigidbody rb;

    private void Awake()
    {
        if (!rb)
        {
            rb = GetComponent<Rigidbody>();
        }
    }

    private void FixedUpdate()
    {
    }

    public void LaunchBall(Vector3 v)
    {
        rb.isKinematic = false;
        rb.AddForce(v);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (tagList.Contains(other.collider.tag))
        {
            if (!masterTentacle)
            {
                masterTentacle = GetComponentInParent<Tentacle>();
                
            }

            rb.isKinematic = true;
            masterTentacle.SetTentacleTarget(transform.position);
        }
        
    }
}
