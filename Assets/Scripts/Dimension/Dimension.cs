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
        dimensionClone.SetActive(false);
    }

    public void TeleportPlayerFrom(string maskName)
    {
        dimensionClone.SetActive(true);
        PlayerHandlerScript.current.TeleportEffect(dimensionClone.RenderTexture,maskName);


        //StartCoroutine(DisableCloneAfterDelay());
    }

    public void TeleportPlayerTo()
    {
        dimensionClone.SetActive(false);
    }

    IEnumerator DisableCloneAfterDelay()
    {
        yield return new WaitForSeconds(2f);
        dimensionClone.SetActive(false);
    }

    public RenderTexture GetDimensionTexture()
    {
        return dimensionClone.RenderTexture;
    }

    public void SetDimensionActive(bool b)
    {
        
        dimensionClone.SetActive(b);
    }
    
}