using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    private PickUpInteractables currentItem;

    [SerializeField]
    private Transform handPosition;

    public Transform HandPosition => handPosition;

    [SerializeField]
    private Transform dropPosition;

    [SerializeField]
    private Transform throwPosition;

    [Tooltip("forward axis for throw direction")]
    [SerializeField]
    private Transform throwVector;

    [SerializeField]
    private float throwSpeed = 10f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        if (!handPosition)
        {
            handPosition = transform;
        }

        if (!dropPosition)
        {
            dropPosition = transform;
        }

        if (!throwPosition)
        {
            throwPosition = transform;
        }

        if (!throwVector)
        {
            throwVector = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickUpItem(PickUpInteractables newItem)
    {
        print($"Player pick up {newItem.name}");
        if (currentItem)
        {
            currentItem.OnDrop();
        }

        currentItem = newItem;
        newItem.OnPickUp(handPosition);
    }

    public void OnDrop()
    {
        if (!currentItem)
        {
            return;
        }
        currentItem.OnDrop();
        currentItem.transform.position = dropPosition.position;
        currentItem = null;
    }

    public void OnThrow()
    {
        if (!currentItem)
        {
            return;
        }
        currentItem.OnThrow(throwVector.forward*throwSpeed);
        currentItem.transform.position = throwPosition.position;
        currentItem = null;

    }
}
