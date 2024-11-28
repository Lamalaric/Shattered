using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    [SerializeField] private int nextScene;

    public void CompleteLevel()
    {
        Debug.Log($"Level {nextScene} completed");
        GameManager.currentSceneSave = nextScene;

        //On affiche la prochaine scène
        SceneManager.LoadScene(nextScene);
    }
}