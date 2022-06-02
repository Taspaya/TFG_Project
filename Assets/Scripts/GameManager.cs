using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
[System.Serializable]
public class MyTimeline
{
    public string name;
    public GameObject myTimeline;
}
public class GameManager : PersistentSingleton<GameManager>
{


    [SerializeField]
    public UI_Manager UI_Manager;

     
    [SerializeField]
    public MyTimeline[] timelines;
    public enum GameState { Playing, Dialogue, Menu}

    GameState currentGameState = GameState.Playing;

    private void Start()
    {
        SceneManager.LoadScene("Lvl 0",LoadSceneMode.Additive);
    }

    public void ChangeGameState(GameState newState)
    {
        currentGameState = newState;
    }

    public GameState GetCurrentGameState() { return currentGameState; }


}
