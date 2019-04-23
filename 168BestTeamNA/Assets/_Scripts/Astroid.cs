using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astroid : DamagableEntity
{
    public int damage;

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
        if(collision.gameObject.GetComponentInParent<DamagableEntity>() != null)
        {
            collision.gameObject.GetComponentInParent<DamagableEntity>().TakeDamage(damage,this.curColor);
            Destroy(this.gameObject);
        }
    }
}
