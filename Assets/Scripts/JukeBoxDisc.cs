using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
}