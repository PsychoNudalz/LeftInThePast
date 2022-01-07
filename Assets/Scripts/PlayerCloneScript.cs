using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCloneScript : MonoBehaviour
{
    private static bool _LOCKON = false; // Override so all clones keeps on
    
    private PlayerHandlerScript player;
    [SerializeField] private Transform head;
    [SerializeField] private Camera camera;
    [SerializeField] private Dimension dimension;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private RenderTexture baseRenderTexture;
    [SerializeField] private RenderTexture renderTexture;

    public RenderTexture RenderTexture => renderTexture;

    public Camera Camera => camera;

    private void Awake()
    {
        //CreateRenderTexture();
        ConnectRenderTexture();
    }

    private void Start()
    {
        StartBehaviour();
    }
[ContextMenu("Start")]
    private void StartBehaviour()
    {
        player = PlayerHandlerScript.current;
        mainCamera = Camera.main;
        dimension = GetComponentInParent<Dimension>();
    }

    private void LateUpdate()
    {
        UpdatePositionAndCamera();
    }



    private void UpdatePositionAndCamera()
    {
        Transform transform2;
        if (!player)
        {
            StartBehaviour();
        }
        (transform2 = transform).position = player.transform.position + DimensionController.Current.GetZDiff(dimension);
        transform2.rotation = player.transform.rotation;
        var transform1 = mainCamera.transform;
        head.rotation = transform1.rotation;
        head.position = transform1.position + DimensionController.Current.GetZDiff(dimension);
    }

    public void SetActive(bool b)
    {
        if (_LOCKON)
        {
            b = true;
        }
        
        gameObject.SetActive(b);
        if (b)
        {
            UpdatePositionAndCamera();
        }
    }
    
    [ContextMenu("Create Render Texture")]

    void CreateRenderTexture()
    {
        renderTexture = new RenderTexture(baseRenderTexture);
        camera.targetTexture = renderTexture;
    }
    [ContextMenu("Connect Texture to Camera")]

    void ConnectRenderTexture()
    {
        camera.targetTexture = renderTexture;
    }
}
