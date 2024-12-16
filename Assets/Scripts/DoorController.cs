using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    [SerializeField] private int nextScene;
    private int nbOfLevels = 3;

    public void CompleteLevel()
    {
        Debug.Log($"Level {nextScene} completed");
        GameManager.currentSceneSave = nextScene;

        //On affiche la prochaine scène (menu de win si on a fait tout les lvl)
        if (nextScene > nbOfLevels) GameManager.UpdateGameState(GameState.Win);
        else SceneManager.LoadScene(nextScene);
    }
}