using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = Unity.Mathematics.Random;
/// <summary>
/// Playing sound(s) on repeat from a random time range
/// added in Ver 10
/// </summary>
public class RepeatableSound : Sound
{
    [Header("Ignore the things Above")]

    [SerializeField]
    private Sound sound;

    [Header("Settings")]
    [SerializeField]
    private Vector2 frequency = new Vector2(1, 1);

    [SerializeField]
    private bool playF = true;
    
    private float randomFrequency = 0;

    private float lastPlayTime = 0;

    private bool isPlaying = false;
    // Start is called before the first frame update
    void Start()
    {
        StartBehaviour();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            PlayRepeatableSound();
        }
    }

    protected override void StartBehaviour()
    {
        // base.StartBehaviour();
        if (sound)
        {
            if (sound is RepeatableSound)
            {
                Debug.LogError($"{name} can not take {sound} as this repeatable cant take repeatable");
            }
        }
    }

    protected override void Initialise()
    {
        // base.Initialise();
    }

    public override bool IsPlaying()
    {
        return isPlaying;
    }

    public override void Pause()
    {
    }

    public override void Resume()
    {
    }

    public override void Play()
    {
        //lastPlayTime = 0;
        isPlaying = true;

    }

    public override void PlayF()
    {
        Play();
    }

    public override void Stop()
    {
        isPlaying = false;
    }

    void PlayRepeatableSound()
    {
        if (Time.time-lastPlayTime > randomFrequency)
        {
            if (playF)
            {
                sound.PlayF();
            }

            else
            {
                sound.Play();
            }
            lastPlayTime = Time.time;
            randomFrequency = UnityEngine.Random.Range(frequency.x, frequency.y);
            
        }
    }
}
