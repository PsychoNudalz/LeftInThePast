using System;
using System.Collections;
using System.Collections.Generic;
using QFSW.QC;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    private static int _MAXFPS = 120;

    private static bool gameWin = false;
    private static bool jukeboxCompleted = false;


    private static bool gameOver = false;
    public static bool GameWin => gameWin;

    public static bool JukeboxCompleted => jukeboxCompleted;
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

    public void SetInstanceJukebox()
    {
        Debug.Log("juke box completed");
        jukeboxCompleted = true;
    }

    public static void SetJukebox()
    {
        current.SetInstanceJukebox();
    }

    public void SetInstanceGameOver()
    {
        gameOver = true;
    }

    public static void SetGameOver()
    {
        current.SetInstanceGameOver();
    }

    public void StartGame()
    {
        Debug.Log("Start Game");
        SceneManager.LoadScene(1);
    }

    public void ReturnToMenu()
    {
        Debug.Log("To Menu");

        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT APPLICATAION");
        Application.Quit();
    }

    [Command("Test1")]
    public void Test1()
    {
        PlayerHandlerScript.SetIgnore();
        Time.timeScale = 5f;
    }
}