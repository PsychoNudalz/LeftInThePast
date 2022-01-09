using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    [Header("Teleport Effects")]
    [SerializeField]
    private GameObject teleportEffectParent;

    [SerializeField]
    private RawImage teleportScreen;

    [SerializeField]
    private Animator maskAnimator;

    [Header("End Screens")]
    [SerializeField]
    private Animator screenAnimator;

    [SerializeField]
    private TextMeshProUGUI timeText;

    public static PlayerUIController current;

    // Start is called before the first frame update
    void Awake()
    {
        current = this;
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
    public void SetTeleportEffect(Texture newTexture, string maskName = "TPEffect1")
    {
        teleportScreen.texture = newTexture;
        maskAnimator.Play(maskName);
    }

    public void ReloadScene()
    {
        GameManagerScript.current.StartGame();
    }

    public void ReturnToMenuScene()
    {
        GameManagerScript.current.ReturnToMenu();
    }

    public void SetTimeText()
    {
        timeText.text = GameManagerScript.current.CompletionTimeString();
    }

    public void PlayStart()
    {
        screenAnimator.Play("ScreenControl_Start");
    }

    public void PlayGameOver()
    {
        screenAnimator.Play("ScreenControl_GameOver");
    }

    public void PlayGameWin()
    {
        screenAnimator.Play("ScreenControl_GameWin");
        SetTimeText();
    }
    
}