using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : PersistentSingleton<GameManager>
{
    [SerializeField]
    public UI_Manager UI_Manager;
    public enum GameState { Playing, Dialogue, Menu}

    GameState currentGameState = GameState.Playing;

    public void ChangeGameState(GameState newState)
    {
        currentGameState = newState;
    }

    public GameState GetCurrentGameState() { return currentGameState; }

}
