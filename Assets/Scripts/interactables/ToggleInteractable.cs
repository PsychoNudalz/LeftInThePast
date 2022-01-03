using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ToggleInteractable : UsableInteractable
{
    [Header("Toggle")]
    [SerializeField]
    private UnityEvent OnUnuse;
    [SerializeField]
    private bool toggleOn;

    [ContextMenu("Toggle")]
    protected override void StartBehaviour()
    {
        base.StartBehaviour();
           
    }

    public override void OnInteract()
    {
        //base.OnInteract();
        if (Time.time - cooldownTime_Last < cooldownTime)
        {
            return;
        }
        cooldownTime_Last = Time.time;
        OnFocus_Exit();
        if (!toggleOn)
        {
            OnUse.Invoke();
            toggleOn = true;
        }
        else
        {
            OnUnuse.Invoke();
            toggleOn = false;

        }
    }
}
