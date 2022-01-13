using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// links the discs the player picks up with discs in the jukebox and the usic they will be playing
/// </summary>
public class JukeBoxDisc : MonoBehaviour
{
    [SerializeField]
    private DiscEnum discEnum;

    public DiscEnum DiscEnum => discEnum;

    [SerializeField]
    private AnimatedShader animatedShader;

    [SerializeField]
    private Sound music;
    // Start is called before the first frame update
    void Start()
    {
        if (!animatedShader)
        {
            animatedShader = GetComponent<AnimatedShader>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAddAnimation()
    {
        animatedShader.Play();
    }

    public void PlayMusic()
    {
        music?.Play();
    }

    public void StopMusic()
    {
        music?.Stop();
    }
    
}
