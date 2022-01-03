using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
