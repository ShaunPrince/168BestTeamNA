using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagableEntity : ColoredEntity
{
    //EColor will be used for identification and damage


    public float health;

    // Start is called before the first frame update
    void Start()
    {
        
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
            }
        }

    }
}
