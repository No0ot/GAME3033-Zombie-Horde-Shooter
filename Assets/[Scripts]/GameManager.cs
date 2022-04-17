using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject inGameUI;
    public GameObject pauseMenu;

    public bool isPaused = false;
    private void Awake()
    {
        instance = this;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        inGameUI.SetActive(false);
        pauseMenu.SetActive(true);
        isPaused = true;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        inGameUI.SetActive(true);
        isPaused = false;
        Cursor.visible = false;
    }
}
