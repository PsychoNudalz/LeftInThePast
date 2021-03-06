using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// script for raycasting from the player to select interactable objects to interact with
/// </summary>
public class PlayerInteract : MonoBehaviour
{
    [SerializeField]
    private Interactable currentObject;

    [Header("Tags and Layers")]
    [SerializeField]
    private List<string> tagList;

    [SerializeField]
    private LayerMask layerMask;

    [Header("Ray")]
    [SerializeField]
    private Transform head;

    public Transform Head => head;

    [SerializeField]
    private float rayRange = 3f;

    [SerializeField]
    private float castIntervals = 0.2f;

    private float lastCast;

    [Header("Player Pick Up")]
    private PlayerInventory playerInventory;

    [Header("Debug")]
    [SerializeField]
    private bool isDebug = false;


    // Start is called before the first frame update
    void Awake()
    {
        if (!playerInventory)
        {
            playerInventory = PlayerHandlerScript.current.PlayerInventory;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (lastCast + castIntervals < Time.time)
        {
            lastCast = Time.time;
            CastInteractionRay();
        }
    }

    void CastInteractionRay()
    {
        RaycastHit hit;
        if (Physics.Raycast(head.position, head.forward, out hit, rayRange, layerMask))
        {
            if (tagList.Contains(hit.collider.tag))
            {
                Interactable i = hit.collider.GetComponentInParent<Interactable>();
                if (!i)
                {
                    i = hit.collider.GetComponentInChildren<Interactable>();
                }
                if (i)
                {
                    SetCurrentObject(i);
                    return;
                }
                else
                {
                    print($"{hit.collider.name} is in focus, but missing interactable script");

                }
            }
        }

        if (currentObject)
        {
            SetCurrentObject(null);
        }
    }

    /// <summary>
    /// sets the object it is in focus with
    /// </summary>
    /// <param name="i"></param>
    void SetCurrentObject(Interactable i)
    {
        if (i is UsableInteractable)
        {
            if (!(i as UsableInteractable).CanInteract())
            {
                return;
            }
        }
        
        if (currentObject)
        {
            currentObject.OnFocus_Exit();
        }

        currentObject = i;
        if (currentObject)
        {
            currentObject.OnFocus_Enter();
        }
    }

    public void OnInteract()
    {
        if (currentObject)
        {
            currentObject.OnInteract();
        }
    }
}