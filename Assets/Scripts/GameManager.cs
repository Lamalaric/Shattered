using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static UnityEvent OnWinGame;
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

    public void UpdateGameState(GameState gameState)
    {
        state = gameState;
        switch (gameState)
        {
            case GameState.MainMenu:
                OnWinGame.Invoke();
                break;
            case GameState.Win :
                break;
            case GameState.Play:
                break;
            default:
                throw new System.Exception("Game state is invalid");
        }

        //Raise an event when changing the gamestate
        //TODO
    }
}


public enum GameState
{
    MainMenu,
    Play,
    Win,
    Lose
}