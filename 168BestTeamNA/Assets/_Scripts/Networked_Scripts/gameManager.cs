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
    private int maxNumPlayers = 4;

    // used to specify variables that are to be synced by the server
    [SyncVar] public GameState gameStatus;

    [SyncVar] public int numPlayers;

    public static Material[] materialList;
    public GameObject[] listPlayers;

    private bool[] recievedPlayerCount;

    public override void OnStartServer()
    {
        numPlayers = 1;
        Debug.Log("Number of players on start: " + numPlayers);
        //Debug.Log(NetworkManager.activeTransport.IsStarted);
        //NetworkServer.Listen(7778);
        //Debug.Log("Registering server callbacks");
        //NetworkClient client = new NetworkClient();
        //client.RegisterHandler(MsgType.Connect, OnConnected);
        //string ip = NetworkManager.singleton.networkAddress;
        //client.Connect(ip, 7777);

        //NetworkServer.RegisterHandler(MsgType.Connect, OnConnected);
        //string ip = NetworkManager.singleton.networkAddress;


        recievedPlayerCount = new bool[maxNumPlayers]; // set up
        for (int i = 0; i < maxNumPlayers; ++i)
        {
            recievedPlayerCount[i] = true;
        }
    }

    void OnConnected(NetworkMessage netMsg)
    {
        Debug.Log("Client connected");
    }


    // Start is called before the first frame update
    void Awake()
    {
        gameStatus = GameState.NotStarted;

        materialList = new Material[5];
        materialList[0] = Resources.Load<Material>("Gray");
        materialList[1] = Resources.Load<Material>("Red");
        materialList[2] = Resources.Load<Material>("Blue");
        materialList[3] = Resources.Load<Material>("Green");
        materialList[4] = Resources.Load<Material>("Yellow");

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

            // if player count has changed,
            AddedPlayer();

            // keep sending message until all players have recieved it >.<
            //if (playerStillMissingPlayerCount())  sendPlayerCount(numPlayers);
        }

    }

    void startGame()
    {
        gameStatus = GameState.Started;
        Debug.Log("Game Starting!");
    }

    public bool gameStarted()
    {
        if (gameStatus == GameState.Started)
        {
            return true;
        }
        else return false;
    }

    public int getNumberPlayers()
    {
        return numPlayers;
    }

    public void addNewPlayer()
    {
        ++numPlayers;
        GameObject.FindWithTag("EnemyManager").GetComponent<EnemyManager>().setNumPlayers(numPlayers);
    }

    public void setPlayerColor(GameObject player)
    {
        if (player.gameObject.GetComponentInChildren<Renderer>() != null)
        {
            player.gameObject.GetComponentInChildren<Renderer>().material = materialList[numPlayers];
        }
    }

    void sendPlayerCount(int num)
    {
        NumberOfPlayers.PlayerCountMessage msg = new NumberOfPlayers.PlayerCountMessage();
        msg.numPlayers = num;

        NetworkServer.SendToAll(NumberOfPlayers.MyMsgType.playerCount, msg);

    }

    void AddedPlayer()
    {
        listPlayers = GameObject.FindGameObjectsWithTag("Player");
        if (listPlayers.Length > numPlayers)
        {
            ++numPlayers;
            RpcUpdateNumberPlayers(numPlayers);

            // need to recieve an ack from each of the existing num players
            //for (int i = 0; i < numPlayers; ++i)
            //{
            //    recievedPlayerCount[i] = false;
            //}

            Debug.Log("Number of players: " + numPlayers);
            //sendPlayerCount(numPlayers);
        }
    }

    private bool playerStillMissingPlayerCount()
    {
        for (int i = 0; i < numPlayers; ++i)
        {
            if (recievedPlayerCount[i] == false) return true;
        }

        return false;
    }

    [ClientRpc]
    void RpcUpdateNumberPlayers(int playerCount)
    {
        numPlayers = playerCount;
        Debug.Log("A thing is happening here");
    }
}
