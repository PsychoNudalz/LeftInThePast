using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dimension : MonoBehaviour
{
    [SerializeField]
    PlayerCloneScript dimensionClone;

    [SerializeField]
    private PatrolManager patrolManager;

    [SerializeField]
    private JukeBox dimensionJukeBox;

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
        //dimensionClone.SetActive(false);
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


        //StartCoroutine(DisableCloneAfterDelay());
    }

    /// <summary>
    /// ending the player teleport
    /// </summary>
    public void TeleportPlayerTo()
    {
        dimensionClone.SetActive(false);
    }

    public RenderTexture GetDimensionTexture()
    {
        return dimensionClone.RenderTexture;
    }

    public void SetDimensionActive(bool b)
    {
        //dimensionClone.SetActive(b);
    }
}