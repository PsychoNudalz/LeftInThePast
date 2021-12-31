using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dimension : MonoBehaviour
{
    [SerializeField] PlayerCloneScript dimensionClone;

    private void Start()
    {
        if (!dimensionClone)
        {
            dimensionClone = GetComponentInChildren<PlayerCloneScript>();
        }
        dimensionClone.SetActive(false);
    }

    public void TeleportPlayerFrom()
    {
        dimensionClone.SetActive(true);
        PlayerHandlerScript.current.TeleportEffect(dimensionClone.RenderTexture);
        
        
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

    RenderTexture GetDimensionTexture()
    {
        return dimensionClone.RenderTexture;
    }

}
