using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DamagableEntity : ColoredEntity
{
    //EColor will be used for identification and damage


    [SyncVar] public float health;
    public GameObject GameOverUI;

    // Start is called before the first frame update
    void Start()
    {
        GameOverUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {

    }

    public virtual void TakeDamage(float dmg , EColor colorOfDamageSource)
    {
        if(colorOfDamageSource == curColor || curColor == EColor.Gray)
        {
            this.health -= dmg;
            if (this.health <= 0)
            {
                Destroy(this.gameObject);
                GameOverUI.SetActive(true);
            }
        }

    }
}
