using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static UnityEvent OnWinGame;
    public static int currentSceneSave = 1;
    public GameState state;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateGameState(GameState.MainMenu);

        //Add listeners
        // OnWinGame.AddListener(GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().OnWinGame);
    }

    public static void UpdateGameState(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.MainMenu:
                OnWinGame.Invoke();
                break;
            case GameState.Win :
                break;
            case GameState.Play:
                break;
            case GameState.Lose:
                OnLoseGame();
                break;
            default:
                throw new System.Exception("Game state is invalid");
        }

        //Raise an event when changing the gamestate
        //TODO
    }

    private static void OnLoseGame()
    {
        SceneManager.LoadScene("Lose menu");
    }
}


public enum GameState
{
    MainMenu,
    Play,
    Win,
    Lose
}