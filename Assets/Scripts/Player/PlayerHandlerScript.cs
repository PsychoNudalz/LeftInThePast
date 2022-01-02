using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.Serialization;

using QFSW.QC;

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
    private PlayerLookingGlass playerLookingGlass;

    [SerializeField]
    private LookingGlass lookingGlass;

    [field: Header("Debug")]
    [field: SerializeField]
    public static bool IgnorePlayer { get; private set; } = false;


    public PlayerInventory PlayerInventory => playerInventory;
    public LookingGlass LookingGlass => lookingGlass;

    public static PlayerHandlerScript Current => current;

    public PlayerTeleportScript PlayerTeleportScript => playerTeleportScript;

    public PlayerInteract PlayerInteract => playerInteract;

    public FirstPersonController FirstPersonController => firstPersonController;

    public PlayerUIController PlayerUIController => playerUIController;

    public PlayerFootstepsScript PlayerFootstepsScript => playerFootstepsScript;
    public PlayerLookingGlass PlayerLookingGlass => playerLookingGlass;

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

        if (!playerLookingGlass)
        {
            playerLookingGlass = GetComponent<PlayerLookingGlass>();
        }
    }

    public void TeleportMovePlayer(Vector3 transformOffset)
    {
        playerLookingGlass.EquipLookingGlass(false);

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

    [Command()]
    public static void SetIgnore(bool b)
    {
        IgnorePlayer = b;
    }

    [Command()]

    public static void SetIgnore()
    {
        IgnorePlayer = !IgnorePlayer;
    }
}