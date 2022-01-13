using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// script on the physics ball to determine where yhe tentacle will stick to
/// </summary>
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

    /// <summary>
    /// set the position it collided as the end point of the tentacle
    /// </summary>
    /// <param name="other"></param>
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
