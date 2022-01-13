using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = Unity.Mathematics.Random;

/// <summary>
/// creates a noise SOI on collision 
/// </summary>
public class OnCollisionSOI : MonoBehaviour
{
    [SerializeField]
    private Vector2 SOIRange = new Vector2(10, 15);
    [SerializeField]
    private List<string> tags;

    private bool disableSound = false;
    private void OnCollisionEnter(Collision other)
    {
        if (disableSound)
        {
            return;
        }
        if (tags.Count == 0|| tags.Contains(other.collider.tag))
        {
            if (MonsterHandlerScript.current)
            {
                MonsterHandlerScript.current.RecieveSoi(new SourceOfInterest(name,transform.position,SourceOfInterestType.Noise,UnityEngine.Random.Range(SOIRange.x,SOIRange.y)));
            }
        }
    }

    public void DelaySOI(float t)
    {
        StartCoroutine(delaySOI(t));
    }
    IEnumerator delaySOI(float t)
    {
        disableSound = true;
        yield return new WaitForSeconds(t);
        disableSound = false;
    }
}
