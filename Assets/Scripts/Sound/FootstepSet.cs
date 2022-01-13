using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Play set of footstep
/// </summary>
public class FootstepSet : MonoBehaviour
{
    [SerializeField]
    private RepeatableSound footstepSet;
    
    public void PlayFootsteps()
    {
        footstepSet.Play();
    }

    public void StopFootsteps()
    {
        footstepSet.Stop();
    }

}
