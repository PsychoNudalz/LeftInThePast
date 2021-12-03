using System.Collections;
using System.Collections.Generic;
using HighlightPlus;
using UnityEngine;

[RequireComponent(typeof(HighlightEffect))]
public abstract class Interactable : MonoBehaviour
{
    [SerializeField]
    protected HighlightEffect highlightEffect;

    // [ContextMenu("OnFocus_Enter")]
    public abstract void OnFocus_Enter();

    public abstract void OnInteract();

    // [ContextMenu("OnFocus_Exit")]
    public abstract void OnFocus_Exit();
}