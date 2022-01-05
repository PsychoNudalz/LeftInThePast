using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DiscEnum
{
    Home,
    Alt,
    Crystal,
    Matrix
}

public class DiscScript : MonoBehaviour
{
    [SerializeField]
    private DiscEnum discEnum;

    public DiscEnum DiscEnum => discEnum;

    [SerializeField]
    private PickUpInteractables pickUpInteractables;
    // Start is called before the first frame update

    private void Awake()
    {
        if (!pickUpInteractables)
        {
            pickUpInteractables = GetComponent<PickUpInteractables>();
            
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSlot()
    {
        if (pickUpInteractables)
        {
            pickUpInteractables.OnPickUp(DimensionController.Current.CurrentDimension.DimensionJukeBox.SlotDiscParent);
            transform.localPosition = new Vector3();
            transform.localRotation = Quaternion.identity;
        }
    }
}
