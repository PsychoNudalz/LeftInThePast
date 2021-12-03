using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    IEnumerator DelayActiveCinemachine()
    {
        yield return new WaitForEndOfFrame();

        CMCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().enabled = true;
    }


}
