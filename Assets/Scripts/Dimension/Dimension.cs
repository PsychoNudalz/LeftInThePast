using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// enum for each dimension
/// </summary>
public enum DimensionEnum
{
    Home,
    Alt,
    Crystal,
    Matrix,
    Final
}
/// <summary>
/// control each dimension
/// </summary>
public class Dimension : MonoBehaviour
{
    [SerializeField]
    private DimensionEnum dimensionEnum;

    public DimensionEnum DimensionEnum => dimensionEnum;

    [SerializeField]
    PlayerCloneScript dimensionClone;

    [SerializeField]
    private PatrolManager patrolManager;

    [SerializeField]
    private JukeBox dimensionJukeBox;

    [SerializeField]
    private DimensionGlitchVFXController[] dimensionGlitchVFXControllers;

    [Header("Sounds")]
    [SerializeField]
    private Sound transitionSound;


    public PlayerCloneScript DimensionClone => dimensionClone;

    public JukeBox DimensionJukeBox => dimensionJukeBox;

    public PatrolManager PatrolManager => patrolManager;

    private void Start()
    {
        if (!dimensionClone)
        {
            dimensionClone = GetComponentInChildren<PlayerCloneScript>();
        }

        if (!patrolManager)
        {
            patrolManager = GetComponentInChildren<PatrolManager>();
        }

        if (!dimensionJukeBox)
        {
            dimensionJukeBox = GetComponentInChildren<JukeBox>();
        }

        if (dimensionGlitchVFXControllers == null || dimensionGlitchVFXControllers.Length == 0)
        {
            dimensionGlitchVFXControllers = GetComponentsInChildren<DimensionGlitchVFXController>();
        }

        //dimensionClone.SetActive(false);
        SetGlitchVFX(false);
    }

    /// <summary>
    /// Preparing the player to teleport
    /// 
    /// </summary>
    /// <param name="maskName"></param>
    public void TeleportPlayerFrom(string maskName)
    {
        dimensionClone.SetActive(true);
        PlayerHandlerScript.current.TeleportEffect(dimensionClone.RenderTexture, maskName);
        SetGlitchVFX(false);


        //StartCoroutine(DisableCloneAfterDelay());
    }

    /// <summary>
    /// ending the player teleport
    /// </summary>
    public void TeleportPlayerTo()
    {
        transitionSound?.PlayF();
        dimensionClone.SetActive(false);
        SetGlitchVFX(true);
    }

    public RenderTexture GetDimensionTexture()
    {
        return dimensionClone.RenderTexture;
    }

    public void SetDimensionActive(bool b)
    {
        //dimensionClone.SetActive(b);
    }

    /// <summary>
    /// disable glitch VFX when leaving the dimension, seeing the vfx from another dimension will cause an infinite glow glitch
    /// </summary>
    /// <param name="b"></param>
    public void SetGlitchVFX(bool b)
    {
        foreach (DimensionGlitchVFXController dimensionGlitchVFXController in dimensionGlitchVFXControllers)
        {
            if (b)
            {
                dimensionGlitchVFXController.Play();
            }
            else
            {
                dimensionGlitchVFXController.Stop();
            }
        }
    }
}