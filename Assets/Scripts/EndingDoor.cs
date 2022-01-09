using System;
using System.Collections;
using System.Collections.Generic;
using QFSW.QC;
using UnityEngine;

public class EndingDoor : MonoBehaviour
{
    [Header("House")]
    [SerializeField]
    private GameObject houseOriginal;

    [SerializeField]
    private GameObject houseFracture;

    [SerializeField]
    private float timeBetweenBreak = 0.1f;
[SerializeField]
    private Rigidbody[] fracturePieces = Array.Empty<Rigidbody>();

    [SerializeField]
    private float timeUntilEnding = 5f;


    private void Awake()
    {
        houseOriginal.SetActive(true);

        if (fracturePieces.Length == 0)
        {
            fracturePieces = houseFracture.GetComponentsInChildren<Rigidbody>();
        }

        foreach (Rigidbody fracturePiece in fracturePieces)
        {
            fracturePiece.useGravity = true;
            fracturePiece.isKinematic = true;
        }

        houseFracture.SetActive(false);
    }

    [ContextMenu("Break")]
    public void StartChainBreak()
    {
        // foreach (PlayerCloneScript playerCloneScript in FindObjectsOfType<PlayerCloneScript>())
        // {
        //     playerCloneScript.SetActive(false);
        // }

        houseOriginal.SetActive(false);
        houseFracture.SetActive(true);
        
        PlayerHandlerScript.FreezePlayer();

        int i = 0;
        StartCoroutine(BreakPiece(i));
    }

    IEnumerator BreakPiece(int i)
    {
        fracturePieces[i].isKinematic = false;
        i++;
        Debug.Log("Break");
        yield return new WaitForSeconds(timeBetweenBreak);
        if (i < fracturePieces.Length)
        {
            StartCoroutine(BreakPiece(i));
        }
        else
        {
            StartCoroutine(StartEnd());
        }
    }

    IEnumerator StartEnd()
    {
        yield return new WaitForSeconds(timeUntilEnding);
        GameManagerScript.SetGameWin();
    }
}