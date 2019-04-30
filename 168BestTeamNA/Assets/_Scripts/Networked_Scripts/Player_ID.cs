using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player_ID : NetworkBehaviour
{
    [SyncVar] private string playerUniqueIdentity;
    private NetworkInstanceId playerNetID;
    private Transform myTransform;

    public override void OnStartLocalPlayer()
    {
        GetNetworkIdentity();
        SetIdentity();
    }

    // Start is called before the first frame update
    void Awake()
    {
        myTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        // update name on server
        if (myTransform.name == "" || myTransform.name == "Player(Clone)" || myTransform.name == "Networked_Player(Clone)") ;
        {
            SetIdentity();
        }
    }

    [Client]
    void GetNetworkIdentity()
    {
        playerNetID = GetComponent<NetworkIdentity>().netId;
        CmdTellServerMyIdentity(MakeUniqueIdentity());
    }

    void SetIdentity()
    {
        if (!isLocalPlayer)
        {
            myTransform.name = playerUniqueIdentity;
        }
        else
        {
            myTransform.name = MakeUniqueIdentity();
        }
    }

    string MakeUniqueIdentity()
    {
        string uniqueID = "Player " + playerNetID.ToString();
        return uniqueID;
    }

    [Command]
    void CmdTellServerMyIdentity(string name)
    {
        playerUniqueIdentity = name;
    }

    public string returnPlayerID()
    {
        return MakeUniqueIdentity();
    }
}
