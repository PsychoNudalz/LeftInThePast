using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.VFX;

public class MonsterEffects : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    [Header("VFX")]
    [SerializeField]
    private VisualEffect vfx_Eyes;
    [SerializeField]
    private VisualEffect vfx_Chase;

    [Header("Volumes")]
    [SerializeField]
    private Volume passiveVolume;

    [SerializeField]
    private Volume chaseVolume;


    [Header("Head Effect")]
    [SerializeField]
    private Animator faceAnimator;

    [Header("Stare")]
    [SerializeField]
    private string eyeOpenAnimation = "MonsterFace_Stare";
    
    
    [SerializeField]
    private string eyeCloseAnimation = "MonsterFace_StareStop";

    [Header("Chase")]
    private bool isChase = false;
    
    [Header("Sounds")]
    [SerializeField]
    private Sound passiveSound;

    [SerializeField]
    private Sound stareSound;

    [SerializeField]
    private Sound chaseSound;
    
    // Start is called before the first frame update
    void Start()
    {
        if (!animator)
        {
            animator = GetComponent<Animator>();
        }
        passiveSound.Play();
        vfx_Chase.Play();
    }

    private void Update()
    {

    }

    public void OnStare_Start()
    {
        faceAnimator.Play(eyeOpenAnimation);
        stareSound.Play();
        vfx_Eyes.Stop();
    }

    public void OnStart_End()
    {
        faceAnimator.Play(eyeCloseAnimation);
        stareSound.Stop();
        vfx_Eyes.Play();

    }

    public void OnChase_Start()
    {
        passiveVolume.gameObject.SetActive(false);
        chaseVolume.gameObject.SetActive(true);
        vfx_Chase.gameObject.SetActive(true);
        vfx_Chase.Play();
        chaseSound.Play();
        isChase = true;
    }
    public void OnChase_End()
    {
        passiveVolume.gameObject.SetActive(true);
        chaseVolume.gameObject.SetActive(false);
        vfx_Chase.Stop();
        chaseSound.Stop();

        isChase = false;
        OnStart_End();
    }
}
