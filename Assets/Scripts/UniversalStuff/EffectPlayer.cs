using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;

public class EffectPlayer : MonoBehaviour
{
    [SerializeField] ParticleSystem[] particleSystems;
    [SerializeField] VisualEffect[] visualEffects;
    [SerializeField] UnityEvent[] unityEvents;

    public void PlayPS(int index)
    {
        if (index < particleSystems.Length)
        {
            particleSystems[index].Stop();
            particleSystems[index].Play();
        }
    }

    public void StopPS(int index)
    {
        if (index < particleSystems.Length)
        {
            particleSystems[index].Stop();
        }
    }

    public void PlayVE(int index)
    {
        if (index < visualEffects.Length)
        {
            visualEffects[index].Stop();
            visualEffects[index].Play();
        }
    }

    public void StopVE(int index)
    {
        if (index < visualEffects.Length)
        {
            visualEffects[index].Stop();
        }
    }

    public void PlayUE(int index)
    {
        if (index < unityEvents.Length)
        {
            unityEvents[index].Invoke();
        }
    }
}
