using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// plays sounds on collision.  will play sound on any collision if no tags are supplied
/// </summary>
public class OnCollisionSound : MonoBehaviour
{
    [SerializeField]
    private Sound collisionSound;

    [SerializeField]
    private List<string> tags;

    private void OnCollisionEnter(Collision other)
    {
        if (tags.Count == 0)
        {
            collisionSound.Play();
            return;
        }

        if (tags.Contains(other.collider.tag))
        {
            collisionSound.Play();
            return;
        }
    }
}
