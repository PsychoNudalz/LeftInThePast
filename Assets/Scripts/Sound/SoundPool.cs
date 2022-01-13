using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;


/// <summary>
/// creates object pool of sounds
/// good for sounds that plays multiple times without restarting it
/// </summary>
public class SoundPool : Sound
{
    [Header("Pool Handling")]
    [SerializeField] AudioSource[] sourcePool;
    [SerializeField] int sourcePoolSize = 6;
    [SerializeField] int poolIndex = -1;

    private void Awake()
    {
        Initialise();
        sourcePool = new AudioSource[sourcePoolSize];
        AudioSource temp;
        string[] ignoreList = { "minVolume", "maxVolume", "rolloffFactor" };
        for (int i = 0; i < sourcePoolSize; i++)
        {
            temp = gameObject.AddComponent<AudioSource>();
            //temp = new AudioSource(source);

            DuplicateObjectScript.CopyPropertiesTo(source, temp, new List<string>(ignoreList));
            //source.clip = clip;
            sourcePool[i] = temp; 
        }

    }
    [ContextMenu("Play")]
    public override void Play()
    {
        poolIndex = (poolIndex + 1) % sourcePoolSize;
        source = sourcePool[poolIndex];
        base.Play();
    }
    [ContextMenu("PlayF")]
    public override void PlayF()
    {
        poolIndex = (poolIndex + 1) % sourcePoolSize;
        source = sourcePool[poolIndex];
        base.PlayF();
    }

    public override void Stop()
    {
        foreach(AudioSource a in sourcePool)
        {
            a.Stop();
        }

        //base.Stop();
    }

    public override void Pause()
    {
        foreach (AudioSource a in sourcePool)
        {
            a.Pause();
        }
    }

    public override void Resume()
    {
        foreach (AudioSource a in sourcePool)
        {
            a.UnPause();
        }
    }

    public void CopyAudioSource(AudioSource a, AudioSource b)
    {
        b.clip = a.clip;
        b.outputAudioMixerGroup = a.outputAudioMixerGroup;
        b.mute = a.mute;
        b.bypassEffects = a.bypassEffects;
        b.bypassListenerEffects = b.bypassListenerEffects;
    }


}
