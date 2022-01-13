using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// communicates from the player to the looking glass
/// </summary>
public class PlayerLookingGlass : MonoBehaviour
{
    [SerializeField]
    private LookingGlass lookingGlass;
    
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private bool equip;

    [Header("Sounds")]
    [SerializeField]
    private Sound equipSound;
    [SerializeField]
    private Sound unequipSound;

    private void Start()
    {
        OnLookingGlass();
        if (!lookingGlass)
        {
            lookingGlass = GetComponentInChildren<LookingGlass>();
        }
    }

    public void OnLookingGlass()
    {
        
        EquipLookingGlass(!equip);
    }

    public void EquipLookingGlass(bool b)
    {
        if (equip == b)
        {
            return;
        }
        equip = b;
        if (equip)
        {
            animator.SetTrigger("Glass_Equip");
        }
        else
        {
            animator.SetTrigger("Glass_Unequip");
        }
    }


    public void Play_Equip()
    {
        equipSound.PlayF();
    }
    
    public void Play_Unequip()
    {
        unequipSound.PlayF();
    }
    
}
