using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    void Start()
    {
        //Subscribe to the mainmenu gamestate event
        //TODO
    }

    private void OnDestroy()
    {
        //Unsubscribe to the mainmenu gamestate event when quit
        //TODO
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(GameManager.currentSceneSave);
    }

    public void WinGame()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
