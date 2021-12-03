using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    [Header("Teleport Effects")] [SerializeField]
    private GameObject teleportEffectParent;

    [SerializeField] private RawImage teleportScreen;
    [SerializeField] private Animator maskAnimator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTeleportEffect(Texture newTexture)
    {
        teleportScreen.texture = newTexture;
        maskAnimator.SetTrigger("Play");
    }
}
