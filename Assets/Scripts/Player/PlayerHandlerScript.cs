using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerHandlerScript : MonoBehaviour
{
    public static PlayerHandlerScript current;

    [SerializeField]
    private PlayerTeleportScript playerTeleportScript;

    [SerializeField]
    private PlayerInteract playerInteract;

    [FormerlySerializedAs("playerPickUpScript")]
    [SerializeField]
    private PlayerInventory playerInventory;

    [SerializeField]
    private FirstPersonController firstPersonController;

    [SerializeField]
    private PlayerUIController playerUIController;

    [SerializeField]
    private PlayerFootstepsScript playerFootstepsScript;

    [SerializeField]
    private LookingGlass lookingGlass;



    public PlayerInventory PlayerInventory => playerInventory;
    public LookingGlass LookingGlass => lookingGlass;

    public static PlayerHandlerScript Current => current;

    public PlayerTeleportScript PlayerTeleportScript => playerTeleportScript;

    public PlayerInteract PlayerInteract => playerInteract;

    public FirstPersonController FirstPersonController => firstPersonController;

    public PlayerUIController PlayerUIController => playerUIController;

    public PlayerFootstepsScript PlayerFootstepsScript => playerFootstepsScript;

    private void Awake()
    {
        current = this;
        Initialise();
    }

    [ContextMenu("Initialise")]
    void Initialise()
    {
        if (!playerTeleportScript)
        {
            playerTeleportScript = GetComponent<PlayerTeleportScript>();
        }

        if (!playerInteract)
        {
            playerInteract = GetComponent<PlayerInteract>();
        }

        if (!firstPersonController)
        {
            firstPersonController = GetComponent<FirstPersonController>();
        }

        if (!playerUIController)
        {
            playerUIController = FindObjectOfType<PlayerUIController>();
        }

        if (!lookingGlass)
        {
            lookingGlass = GetComponentInChildren<LookingGlass>();
        }

        if (!playerFootstepsScript)
        {
            playerFootstepsScript = FindObjectOfType<PlayerFootstepsScript>();
        }

        if (!playerInventory)
        {
            playerInventory = GetComponent<PlayerInventory>();
        }
    }

    public void TeleportMovePlayer(Vector3 transformOffset)
    {
        playerTeleportScript.TeleportMovePlayer(transformOffset);
    }

    public GameObject GetHead()
    {
        return firstPersonController.CinemachineCameraTarget;
    }

    public void TeleportEffect(RenderTexture previousTexture)
    {
        playerUIController.SetTeleportEffect(previousTexture);
        lookingGlass.SetLookingGlass(previousTexture);
    }

    public void SetPlayerInvincable(bool b)
    {
        if (b)
        {
            tag = "Player";
        }
    }
}