using System.Collections;
using System.Collections.Generic;
using HighlightPlus;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
public class PickUpInteractables : Interactable
{
    // Start is called before the first frame update

    [SerializeField]
    private Transform handPosition;
    
    [SerializeField]
    private float velocityMultiplier = 1f;

    [SerializeField]
    private Vector3 throwTorque = new Vector3(50f,0,0);

    [SerializeField]
    private Rigidbody rb;
    

    void Start()
    {
        if (!rb)
        {
            rb = GetComponent<Rigidbody>();
        }

        if (!highlightEffect)
        {
            highlightEffect = GetComponent<HighlightEffect>();
        }

        if (!handPosition)
        {
            handPosition = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public override void OnFocus_Enter()
    {
        highlightEffect.SetHighlighted(true);

    }
    [ContextMenu("Interact")]

    public override void OnInteract()
    {
        PlayerHandlerScript.current.PlayerInventory.PickUpItem(this);
    }

    public override void OnFocus_Exit()
    {
        highlightEffect.SetHighlighted(false);

    }

   public  void OnPickUp(Transform newParent)
    {
        SetAllColliders(false);
        rb.isKinematic = true;
        transform.parent = newParent;
        OrientateItem();
    }

   public void OnDrop()
   {
       SetAllColliders(true);
       rb.isKinematic = false;
       transform.parent = null;
   }
   public void OnThrow(Vector3 velocity)
   {
       OnDrop();
       rb.velocity = (velocity*velocityMultiplier);
       rb.AddTorque(throwTorque);
   }
   
   
    

    void SetAllColliders(bool b)
    {
        foreach (Collider componentsInChild in GetComponentsInChildren<Collider>())
        {
            componentsInChild.enabled = b;
        }
    }

     void OrientateItem()
    {
        transform.localPosition = new Vector3();
        transform.localRotation = new Quaternion();
        transform.position += transform.position-handPosition.position;
        transform.rotation = handPosition.rotation;
    }
}
