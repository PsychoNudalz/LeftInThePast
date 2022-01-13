using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Teleporting the player
/// </summary>
public class PlayerTeleportScript : MonoBehaviour
{
    [SerializeField] CharacterController characterController;
    [SerializeField] Transform CMCamera;
    [SerializeField] Camera camera;
    // Start is called before the first frame update
    void Awake()
    {
        if (!characterController)
        {
            characterController = GetComponent<CharacterController>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void TeleportPlayer(Vector3 position)
    {
        characterController.enabled = false;
        transform.position = position;
        CMCamera.position = position;
        camera.transform.position = position;

        characterController.enabled = true;
    }

    /// <summary>
    /// Main teleport method for teleporting the player, janky but need to disable and enable a lot of stuff and forcing
    /// positions.
    /// </summary>
    /// <param name="move"></param>
    public void TeleportMovePlayer(Vector3 move)
    {
        characterController.enabled = false;

        transform.position += move;
        characterController.enabled = true;

        CMCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().enabled = false;
        CMCamera.position += move;
        camera.transform.position += move;
        StartCoroutine(DelayActiveCinemachine());
    }


    /// <summary>
    /// have to disable and reenable the CinemaMachine's camera for a frame to teleport the camera instead of it moving smoothly.
    /// </summary>
    /// <returns></returns>
    IEnumerator DelayActiveCinemachine()
    {
        yield return new WaitForEndOfFrame();

        CMCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().enabled = true;
    }


}
