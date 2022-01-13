using System.Collections;
using System.Collections.Generic;
using HighlightPlus;
using UnityEngine;

/// <summary>
/// interactable superclass
/// </summary>
[RequireComponent(typeof(HighlightEffect))]
public abstract class Interactable : MonoBehaviour
{
    [SerializeField]
    protected HighlightEffect highlightEffect;

    // [ContextMenu("OnFocus_Enter")]
    public abstract void OnFocus_Enter();//mainly for activating the highlight and any extra effects

    public abstract void OnInteract();//the main thing

    // [ContextMenu("OnFocus_Exit")]
    public abstract void OnFocus_Exit();//mainly for deactivating the highlight and any extra effects
}