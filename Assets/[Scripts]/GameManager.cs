using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject inGameUI;
    public GameObject pauseMenu;

    public GameObject winMenu;
    public GameObject loseMenu;

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

    public void WinGame()
    {
        Time.timeScale = 0;
        inGameUI.SetActive(false);
        Cursor.visible = true;
        winMenu.SetActive(true);
    }

    public void LoseGame()
    {
        Time.timeScale = 0;
        inGameUI.SetActive(false);
        Cursor.visible = true;
        loseMenu.SetActive(true);
    }

    public void PlayAgainButtonPressed()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("GameplayScene");
    }
}
