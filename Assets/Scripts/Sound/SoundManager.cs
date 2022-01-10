using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// 10th generation of the sound manager
/// </summary>
public class SoundManager : MonoBehaviour
{
    public static SoundManager current;
 
    [SerializeField] List<Sound> sounds = new List<Sound>();
    [SerializeField] List<Sound> soundsCache = new List<Sound>();
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] bool playBGM = true;
    [SerializeField] Sound bgm;

    private void Awake()
    {
        if (current)
        {
            Destroy(current);
        }
        current = this;
    }

    private void Start()
    {
        UpdateSounds();
        if (bgm != null && playBGM)
        {
            if (!bgm.IsPlaying())
            {
                bgm.Play();
            }
        }
    }


    [ContextMenu("Update Sounds")]
    public void UpdateSounds()
    {
        List<Sound> newSounds = new List<Sound>(FindObjectsOfType<Sound>());
        sounds = new List<Sound>();
        foreach (Sound s in newSounds)
        {
            AddSounds(s);
            s.SoundManager = this;
        }
    }

    public void AddSounds(Sound s)
    {
        if (!sounds.Contains(s))
        {
            if (s.AudioMixer == null)
            {
                s.AudioMixer = audioMixer;
            }
            sounds.Add(s);
        }
    }

    public void PauseAllSounds()
    {
        UpdateSounds();
        soundsCache = new List<Sound>();
        foreach (Sound s in sounds)
        {
            if (s.IsPlaying())
            {
                soundsCache.Add(s);
                s.Pause();
            }
        }
    }

    public void ResumeSounds()
    {

        foreach (Sound s in soundsCache)
        {
            s.Resume();
        }
        UpdateSounds();
    }


    public void StopAllSounds()
    {
        soundsCache = new List<Sound>();
        foreach (Sound s in sounds)
        {
            if (s!= null && s.IsPlaying())
            {
                soundsCache.Add(s);
                s.Stop();
            }
        }
    }

    private void OnDestroy()
    {
        StopAllSounds();
    }
}
