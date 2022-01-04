using System.Collections;
using System.Collections.Generic;
using HighlightPlus;
using UnityEngine;
using UnityEngine.Events;

public class UsableInteractable : Interactable
{
    public UnityEvent OnUse;

    [SerializeField]
    protected float cooldownTime;

    // [SerializeField]
    protected float cooldownTime_Last;
    // Start is called before the first frame update
    void Start()
    {
        StartBehaviour();
    }

    protected virtual void StartBehaviour()
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
        if (Time.time - cooldownTime_Last < cooldownTime)
        {
            return;
        }
        OnFocus_Exit();
        OnUse.Invoke();
        RefreshCooldown();
    }

    public void RefreshCooldown()
    {
        cooldownTime_Last = Time.time;
    }

    [ContextMenu("Test/Exit")]

    public override void OnFocus_Exit()
    {
        
        highlightEffect?.SetHighlighted(false);

    }
}
