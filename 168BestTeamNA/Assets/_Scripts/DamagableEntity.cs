using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DamagableEntity : ColoredEntity
{
    //EColor will be used for identification and damage


    [SyncVar] public float health;
    private bool hasTakenDmg = false;
    //public GameObject GameOverUI;

    // Start is called before the first frame update
    void Start()
    {
        //GameOverUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.GetComponent<NetworkIdentity>().isServer)
        {
            RpcUpdateFloorHealth(health);
        }
        
        //if (this.gameObject == GameObject.Find("Floor") && hasTakenDmg)
        //{
        //    RpcUpdateFloorHealth(health);
        //    hasTakenDmg = false;
        //}
    }

    private void OnCollisionEnter(Collision collision)
    {

    }

    public virtual void TakeDamage(float dmg , EColor colorOfDamageSource)
    {
        if(colorOfDamageSource == curColor || curColor == EColor.Gray)
        {
            this.health -= dmg;
            hasTakenDmg = true;  // only used for the floor in order to update it accross clients
            if (this.health <= 0)
            {
                Destroy(this.gameObject);
            }
        }

    }

    [ClientRpc]
    void RpcUpdateFloorHealth(float currentHealth)
    {
        health = currentHealth;
    }
}
