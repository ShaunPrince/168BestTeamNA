using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class PlayerManager : NetworkBehaviour
{
    public int moveSpeed;
    public Camera myCam;
    public Material[] materialList;

    [SyncVar] public int myUniqueColor;
    public int numPlayers;

    public NetworkClient myClient;

    private NumberOfPlayers PlayerCountMsg;

    private int myColor;
    public Renderer myRenderer;
    private bool setColor = false;
    private int setColorCounter = 0;
    private int setColorCounterMax = 10;

    private Rigidbody m_Rigidbody;
    //private PlayerShooter ShootScript;

    public override void OnStartLocalPlayer()
    {
        m_Rigidbody = this.GetComponent<Rigidbody>();
        myCam = Camera.main;
    
        //ShootScript = this.GetComponent<PlayerShooter>();

        //PlayerCountMsg = new NumberOfPlayers();
        //PlayerCountMsg.SetupClient("127.0.0.1", 7777);
        //string ip = NetworkManager.singleton.networkAddress;
        //SetupClient(ip, 7778);

        //numPlayers = GameObject.FindWithTag("GameManager").GetComponent<gameManager>().getNumberPlayers();

        //AddNewPlayer();
        //GetNetworkColor();
        //SetNetworkedColor();

        // set player color
        //GameObject.FindWithTag("GameManager").GetComponent<gameManager>().addNewPlayer();
        //GameObject.FindWithTag("GameManager").GetComponent<gameManager>().setPlayerColor(this.gameObject);

        Debug.Log("Local player set up");
    }

    void Awake()
    {
     
    }

    void Update()
    {
        if (isLocalPlayer)
        {
            UpdatePlayerState();
            myCam.transform.position = new Vector3(this.transform.position.x, myCam.transform.position.y, myCam.transform.position.z);
        }

        if (!setColor)
        {
            numPlayers = GameObject.FindWithTag("GameManager").GetComponent<gameManager>().getNumberPlayers();
            GetNetworkColor();
            SetNetworkedColor();
            if (setColorCounter >= setColorCounterMax) setColor = true;
            else ++setColorCounter;
            
        }

        //numPlayers = GameObject.FindWithTag("GameManager").GetComponent<gameManager>().getNumberPlayers();
    }

    [Client]
    public void UpdatePlayerState()
    {
        UpdateMovement();
        //if (Input.GetMouseButtonDown(0))
            //ShootScript.Shoot();
    }

    [Client]
    private void UpdateMovement()
    {
        float mov_x = Input.GetAxisRaw("Horizontal");
        m_Rigidbody.AddForce(new Vector3(mov_x * moveSpeed, 0.0f, 0.0f));
    }

    void GetNetworkColor()
    {
        myColor = numPlayers;
        CmdTellServerMyColor(myColor);
        Debug.Log(this.GetComponent<Player_ID>().returnPlayerID() + " color set to " + myColor);
    }

    void SetNetworkedColor()
    {
        myRenderer.material = materialList[myUniqueColor];
    }

    /*
    void AddNewPlayer()
    {
        myColor = numPlayers;
        CmdTellServerMyColor(myColor);
    }*/
    /*
    [Command]
    void CmdTellServerToAddPlayer(int num)
    {
        numPlayers = num;
    }*/

    [Command] 
    void CmdTellServerMyColor(int numColor)
    {
        myUniqueColor = numColor;
    }

    public Material GetPlayerColor()
    {
        return myRenderer.material;
    }

    public int GetPlayerColorInt()
    {
        return myUniqueColor;
    }


    /////////////////////////////////////////////

  /*
    public class MyMsgType
    {
        public static short playerCount = MsgType.Highest + 1;
    };

    public class PlayerCountMessage : MessageBase
    {
        public int numPlayers;
    }

    public void SetupClient(string ip, int port)
    {
        myClient = new NetworkClient();
        myClient.RegisterHandler(MsgType.Connect, OnConnected);
        myClient.RegisterHandler(MyMsgType.playerCount, OnPlayerCount);
        myClient.Connect(ip, port);
    }

    public void OnPlayerCount(NetworkMessage netMsg)
    {
        PlayerCountMessage msg = netMsg.ReadMessage<PlayerCountMessage>();
        numPlayers = msg.numPlayers;
        GetNetworkColor();
        Debug.Log("onPlayerCount " + msg.numPlayers);
    }

    public void OnConnected(NetworkMessage netMsg)
    {
        Debug.Log("Connected to server");
    }
    */
}
