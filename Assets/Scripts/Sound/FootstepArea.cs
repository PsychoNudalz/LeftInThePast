using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sets the footstop sounds when entering the trigger area
/// </summary>
public class FootstepArea : MonoBehaviour
{
    [SerializeField]
    private FootstepSet footstepSet;

    public FootstepSet FootstepSet => footstepSet;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHandlerScript.current.SetNewSet(footstepSet);
        }
    }
}
