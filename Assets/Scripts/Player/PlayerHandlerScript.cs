using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.Serialization;
using QFSW.QC;
using UnityEngine.InputSystem;

/// <summary>
/// Main script for handling the player, all scripts outside the player will need to communicate through this
/// </summary>
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

    /// <summary>
    /// plays the teleport effects
    /// </summary>
    /// <param name="previousTexture"></param>
    /// <param name="maskName"></param>
    public void TeleportEffect(RenderTexture previousTexture, string maskName = "TPEffect1")
    {
        playerUIController.SetTeleportEffect(previousTexture, maskName);
        lookingGlass.SetLookingGlass(previousTexture);
    }

    /// <summary>
    /// set new set of foot steps
    /// </summary>
    /// <param name="newFootstepSet"></param>
    public void SetNewSet(FootstepSet newFootstepSet)
    {
        playerFootstepsScript.SetNewSet(newFootstepSet);
    }

    public static Vector3 GetPlayerLookingDir()
    {
        return current.playerInteract.Head.forward;
    }

    public static void DisableGravity()
    {
        current.firstPersonController.OverrideGravity();
    }

    public static void FreezePlayer()
    {
        DisableGravity();
        //current.GetComponent<PlayerInput>().enabled = false;
        current.firstPersonController.MoveSpeed = 0;
        current.firstPersonController.SprintSpeed = 0;
    }

    /// <summary>
    /// Set the Player to be ignored by the AI, can not be detected by tentacles, but will trigger game over if
    ///touches the monster’s main body. Used for exploring the house without getting chased.
    /// </summary>
    /// <param name="b"></param>
    [Command()]
    public static void SetIgnore(bool b)
    {
        IgnorePlayer = b;
    }
/// <summary>
/// toggles ignore
/// </summary>
    [Command()]
    public static void SetIgnore()
    {
        IgnorePlayer = !IgnorePlayer;
    }
}