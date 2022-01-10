using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = Unity.Mathematics.Random;
/// <summary>
/// play random sound from pool of sound
/// added in Ver 9
/// </summary>
public class RandomSound : Sound
{
    [Header("Ignore the things Above")]
    
    [SerializeField]
    private Sound[] sounds;

    private int seed = 0;

    protected override void StartBehaviour()
    {
        //base.StartBehaviour();
    }

    protected override void Initialise()
    {
        if (soundManager != null)
        {
            soundManager = SoundManager.current;
        }
        seed = (int) UnityEngine.Random.Range(0, 100f);
        if (sounds.Length == 0)
        {
            SetAllChildrenSounds();
        }
    }

    [ContextMenu("Set all children sounds")]
    public void SetAllChildrenSounds()
    {
        List<Sound> temp = new List<Sound>();
        foreach (Sound s in GetComponentsInChildren<Sound>())
        {
            if (!s.Equals(this))
            {
                temp.Add(s);
            }
        }

        sounds = temp.ToArray();
    }

    public override void Pause()
    {
        base.Pause();
    }

    public override void Resume()
    {
        base.Resume();
    }

    public override bool IsPlaying()
    {
        return GetRandomSound().IsPlaying();
    }

    public override void Play()
    {
        GetRandomSound().Play();
        
    }

    public override void PlayF()
    {
        GetRandomSound().PlayF();        
    }

    public override void Stop()
    {
        GetRandomSound().Stop();
    }

    /// <summary>
    /// Gets a random sound, will prioritize getting sounds that is not playing
    /// </summary>
    /// <returns></returns>
    Sound GetRandomSound()
    {
        // seed++;
        // seed = seed % sounds.Length;
        //
        seed = UnityEngine.Random.Range(0, sounds.Length );
        Sound temp = sounds[seed];
        for (int i = 0; i < sounds.Length; i++)
        {
            temp = sounds[(seed + i) % sounds.Length];
            if (!temp.Source.isPlaying )
            {
                return temp;
            }
        }

        return temp;
    } 
}
