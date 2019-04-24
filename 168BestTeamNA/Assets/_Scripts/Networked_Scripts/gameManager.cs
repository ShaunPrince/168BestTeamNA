using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public enum GameState
{
    NotStarted,
    Inactive,
    Started,
    EndGame
}

public class gameManager : NetworkBehaviour
{

    [SyncVar]       // used to specify variables that are to be synced by the server
    public GameState gameStatus;

    // Start is called before the first frame update
    void Awake()
    {
        gameStatus = GameState.NotStarted;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isServer)
        {
            if (Input.GetKeyDown("space"))
            {
                startGame();
            }
        }
        
    }

    void startGame()
    {
        gameStatus = GameState.Started;
        Debug.Log("Game Starting!");
    }

    public bool gameStarted()
    {
        if (gameStatus == GameState.Started) return true;
        else return false;
    }
}
