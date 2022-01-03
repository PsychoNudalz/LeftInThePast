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
    
    
    /// <summary>
    /// Set the teleport texture, mask name for different masks
    ///
    /// TPEffect1: Circle
    /// TPEffect2: Shatter
    /// </summary>
    /// <param name="newTexture"></param>
    /// <param name="maskName"></param>
    public void SetTeleportEffect(Texture newTexture,string maskName = "TPEffect1")
    {
        teleportScreen.texture = newTexture;
        maskAnimator.Play(maskName);
    }
}
