using System.Collections;
using System.Collections.Generic;
using HighlightPlus;
using UnityEngine;
using UnityEngine.Events;

public class UsableInteractable : Interactable
{
    public UnityEvent OnUse;
    // Start is called before the first frame update
    void Start()
    {
        if (!highlightEffect)
        {
            highlightEffect = GetComponent<HighlightEffect>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("Test/Enter")]
    public override void OnFocus_Enter()
    {
        highlightEffect.SetHighlighted(true);
    }

    public override void OnInteract()
    {
        OnFocus_Exit();
        OnUse.Invoke();
    }
    [ContextMenu("Test/Exit")]

    public override void OnFocus_Exit()
    {
        highlightEffect.SetHighlighted(false);

    }
}
