using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationDebug : MonoBehaviour
{
    public Animator dresser, wardrobe, nightStand;
    public KeyCode dresserCycle, wardrobeCycle, nightStandCycle;
    int i, j, k;

    private void Start()
    {
        i = j = k = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(dresserCycle))
        {
            if (i == 0)
                dresser.Play("OpenT");
            else if (i == 1)
                dresser.Play("CloseT");
            else if (i == 2)
                dresser.Play("OpenB");
            else if (i == 3)
                dresser.Play("CloseB");
            if (i == 3)
                i = 0;
            else
                i++;
        }

        if (Input.GetKeyDown(wardrobeCycle))
        {
            if (j == 0)
                wardrobe.Play("OpenR");
            else if (j == 1)
                wardrobe.Play("CloseR");
            else if (j == 2)
                wardrobe.Play("OpenL");
            else if (j == 3)
                wardrobe.Play("CloseL");
            if (j == 3)
                j = 0;
            else
                j++;
        }

        if (Input.GetKeyDown(nightStandCycle))
        {
            if (k == 0)
                nightStand.Play("Open");
            else if (k == 1)
                nightStand.Play("Close");
            if (k == 1)
                k = 0;
            else
                k++;
        }
    }
}
