using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class OnCollisionSOI : MonoBehaviour
{
    [SerializeField]
    private Vector2 SOIRange = new Vector2(10, 15);
    [SerializeField]
    private List<string> tags;
    private void OnCollisionEnter(Collision other)
    {
        if (tags.Count == 0|| tags.Contains(other.collider.tag))
        {
            MonsterHandlerScript.current.RecieveSOI(new SourceOfInterest(name,transform.position,SourceOfInterestType.Noise,UnityEngine.Random.Range(SOIRange.x,SOIRange.y)));
        }
    }
}
