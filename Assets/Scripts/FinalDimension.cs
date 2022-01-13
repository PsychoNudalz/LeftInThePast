using System;
using System.Collections;
using System.Collections.Generic;
using QFSW.QC;
using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// custom script to control the final dimension and ending
/// </summary>
public class FinalDimension : MonoBehaviour
{
    [Header("House")]
    [SerializeField]
    private GameObject houseOriginal;

    [SerializeField]
    private GameObject houseFracture;

    [SerializeField]
    private Vector2 timeBetweenBreak = new Vector2(0.05f, .2f);

    [SerializeField]
    private Rigidbody[] fracturePieces = Array.Empty<Rigidbody>();

    [SerializeField]
    private float timeUntilEnding = 5f;

    [Space(10)]
    [SerializeField]
    private GameObject fractureSound;

    //private List<Sound> breakSounds = new List<Sound>();


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

        AddSoundsToPieces();

        houseFracture.SetActive(false);
    }

    /// <summary>
    /// starts collapsing the level
    /// </summary>
    [ContextMenu("Break")]
    public void StartChainBreak()
    {
        // foreach (PlayerCloneScript playerCloneScript in FindObjectsOfType<PlayerCloneScript>())
        // {
        //     playerCloneScript.SetActive(false);
        // }

        houseOriginal.SetActive(false);
        houseFracture.SetActive(true);

        //PlayerHandlerScript.FreezePlayer();

        int i = 0;
        StartCoroutine(BreakPiece(i));
    }

    /// <summary>
    /// recursively calls to break each piece
    /// </summary>
    /// <param name="i"> index value of the piece</param>
    /// <returns></returns>
    IEnumerator BreakPiece(int i)
    {
        fracturePieces[i].isKinematic = false;
        fracturePieces[i].GetComponentInChildren<RandomSound>()?.Play();
        i++;
        Debug.Log("Break");
        yield return new WaitForSeconds(UnityEngine.Random.Range(timeBetweenBreak.x, timeBetweenBreak.y));
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


    void AddSoundsToPieces()
    {
        GameObject temp;
        MeshCollider mesh;
        foreach (Rigidbody fracturePiece in fracturePieces)
        {
            temp = Instantiate(fractureSound, fracturePiece.transform);
            temp.transform.localPosition = new Vector3();
            temp = temp.GetComponentInChildren<OnCollisionSound>().gameObject;
            temp.AddComponent(typeof(MeshCollider));
            mesh = temp.GetComponent<MeshCollider>();
            mesh.sharedMesh = fracturePiece.GetComponent<MeshCollider>().sharedMesh;
            mesh.convex = true;
        }
    }
}