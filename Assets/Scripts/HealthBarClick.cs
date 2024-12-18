using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarClick : MonoBehaviour
{
    public MinigameManager miniGameManager; // Référence au script du mini-jeu

    public void StartMinigame()
    {
        if (miniGameManager != null)
        {
            if (miniGameManager.miniGameActive == false) miniGameManager.ShowMiniGameCanvas();
            else miniGameManager.EndMiniGame();
        }
        else
        {
            Debug.LogError("MiniGameManager non assigné sur la barre de vie !");
        }
    }
}
