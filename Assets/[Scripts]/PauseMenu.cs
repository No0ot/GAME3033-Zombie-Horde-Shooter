using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

   public void ResumeButtonPressed()
   {
        GameManager.instance.ResumeGame();
   }

    public void MainMenuButtonPressed()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartMenu");
    }

    public void QuitButtonPressed()
    {
        Application.Quit();
    }
}
