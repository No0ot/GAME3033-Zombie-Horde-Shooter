using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class MainMenu : MonoBehaviour
{
    public Image backgroundImage;
    public TMP_Text anyButtonText;

    public GameObject startMenu;
    public GameObject mainMenu;

    bool inStartMenu = true;


    private void Update()
    {

        if(inStartMenu)
        {
            anyButtonText.color = new Color(1, 1, 1,Mathf.PingPong(Time.time, 1f));
            if (Keyboard.current.anyKey.wasPressedThisFrame)
                TransistionFromStartToMain();
        }
    }

    public void PlayButtonPressed()
    {
        SceneManager.LoadScene("GameplayScene");
    }

    public void QuitButtonPressed()
    {
        Application.Quit();
    }

    void TransistionFromStartToMain()
    {
        startMenu.SetActive(false);
        mainMenu.SetActive(true);
        inStartMenu = false;
        backgroundImage.color = new Color(1, 1, 1, 0.5f);
    }
}
