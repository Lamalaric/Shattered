using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject quitConfirmationPopup;


    private System.Action confirmAction;
    

    public void ConfirmAction()
    {
        if (confirmAction != null)
        {
            confirmAction.Invoke();
            confirmAction = null;
        }
        quitConfirmationPopup.SetActive(false);
    }

    public void CancelAction()
    {
        quitConfirmationPopup.SetActive(false);
    }
    public void Pause()
    {
        pauseMenu.SetActive(true);  
        Time.timeScale = 0;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;

    }

    public void Settings()
    {

    }

    public void MainMenu()
    {
        PersistentMusic musicManager = FindObjectOfType<PersistentMusic>();
        if (musicManager != null)
        {
            musicManager.StopMusic(); // Arrête la musique
        }
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1;
    }

    public void Quit()
    {
        Application.Quit();
    }
}