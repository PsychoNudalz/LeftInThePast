using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeepingAngel : MonoBehaviour
{
    [Header("Player")]
    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private string player = "Player";

    [Header("Weeping")]
    // [SerializeField]
    // private bool lockX = true;
    //
    // [SerializeField]
    // private bool lockY = false;
    // [SerializeField]
    // private bool lockZ = true;
    [SerializeField]
    private Vector3 offset = new Vector3(0,1f,0);

    [SerializeField]
    private float dotThresshold = 0.7f;
    private bool wasLooked;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;


        // if (Physics.Raycast(transform.position+offset, DirToPlayer(), out hit, 30f, layerMask))
        // {
        //     if (hit.collider.CompareTag(player))
        //     {
        //         if (!wasLooked)
        //         {
        //             wasLooked = true;
        //             RotateToPlayer();
        //         }
        //     }
        // }
        // else
        // {
        //     if (wasLooked)
        //     {
        //         wasLooked = false;
        //     }
        // }

        if (Vector3.Dot(DirToPlayer(),PlayerHandlerScript.GetPlayerLookingDir())<dotThresshold)
        {
            if (!wasLooked)
            {
                RotateToPlayer();
            }

            wasLooked = true;
        }
        else
        {
            if (wasLooked)
            {
                wasLooked = false;
            }
        }
        
    }

    Vector3 DirToPlayer()
    {
        return PlayerHandlerScript.current.transform.position - transform.position;
    }

    void RotateToPlayer()
    {
        Vector3 temp = DirToPlayer();
        temp.y = 0;
        transform.forward = temp;
    }
}