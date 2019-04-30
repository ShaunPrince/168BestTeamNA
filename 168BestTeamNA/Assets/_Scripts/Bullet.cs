using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : ColoredEntity
{
    public int damage;
    private ColoredEntity.EColor playerColor;
    private bool gotColor = false;
    //public float yVelocity;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ReColor(curColor);


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<DamagableEntity>() != null)
        {
            
            collision.gameObject.GetComponent<DamagableEntity>().TakeDamage(damage, this.curColor);
            Destroy(this.gameObject);
        }

        //if (collision.gameObject.GetComponent<Bullet>() != null)
        //{
        //    Destroy(collision.gameObject);
        //    Destroy(this.gameObject);
        //}

        if (!collision.collider.tag.Equals("Projectile"))
        {
            Destroy(this.gameObject);
        }
    }
}
