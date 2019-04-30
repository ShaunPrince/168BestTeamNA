using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NumberOfPlayers : MonoBehaviour
{
    NetworkClient myClient;

    public class MyMsgType
    {
        public static short playerCount = MsgType.Highest + 1;
    };

    public class PlayerCountMessage : MessageBase
    {
        public int numPlayers;
    }

    public void SendPlayerCount(int players)
    {
        PlayerCountMessage msg = new PlayerCountMessage();
        msg.numPlayers = players;

        NetworkServer.SendToAll(MyMsgType.playerCount, msg);
    }

    // Create a client and connect to the server port
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
        Debug.Log("onPlayerCount " + msg.numPlayers);
    }

    public void OnConnected(NetworkMessage netMsg)
    {
        Debug.Log("Connected to server");
    }
}
