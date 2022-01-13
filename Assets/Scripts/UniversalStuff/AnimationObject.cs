using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls an animator by passing a string
/// Mostly for playing an animation in a unity event
/// </summary>
public class AnimationObject : MonoBehaviour
{

    [SerializeField] Animator animator;

    private void Awake()
    {
        if (animator== null)
        {
            animator = GetComponent<Animator>();
        }
    }

    public void Play_Trigger(string s)
    {
        animator.SetTrigger(s);
    }
    /// <summary>
    /// Play int variable
    /// (Parameter name)/(Value)
    /// </summary>
    /// <param name="s"></param>
    public void Play_Int(string s)
    {
        string[] temp = s.Split('/');
        animator.SetInteger(temp[0], int.Parse(temp[1]));
    }
    /// <summary>
    /// Play bool variable
    /// (Parameter name)/(Value)
    /// 0 = false, 1 = true
    /// </summary>
    /// <param name="s"></param>
    public void Play_Bool(string s)
    {
        string[] temp = s.Split('/');
        if (int.Parse(temp[1]) == 0)
        {
            animator.SetBool(temp[0], false);
        }
        else
        {
            animator.SetBool(temp[0], true);
        }
    }
    /// <summary>
    /// Play float variable
    /// (Parameter name)/(Value)
    /// </summary>
    /// <param name="s"></param>
    public void Play_Float(string s)
    {
        string[] temp = s.Split('/');
        animator.SetFloat(temp[0], float.Parse(temp[1]));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    public void Play_Animation(string s)
    {
        animator.Play(s);
    }

}