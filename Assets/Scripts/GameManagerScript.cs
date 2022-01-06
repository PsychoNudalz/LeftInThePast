using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    private static int _MAXFPS = 120;
    
    private static bool gameWin = false;

    private static bool gameOver = false;
    public static bool GameWin => gameWin;

    public static bool GameOver => gameOver;

    public static GameManagerScript current;

    private void Awake()
    {
        if (!current)
        {
            current = this;
        }

        else
        {
            Destroy(gameObject);
        }

        Application.targetFrameRate = _MAXFPS;
    }
    public void SetInstanceGameWin()
    {
        Debug.Log("Game Win");
        gameWin = true;
    }

    public static void SetGameWin()
    {
        current.SetInstanceGameWin();
    }
    public void SetInstanceGameOver()
    {
        gameOver = true;
    }

    public static void SetGameOver()
    {
        current.SetInstanceGameOver();
    }
}
