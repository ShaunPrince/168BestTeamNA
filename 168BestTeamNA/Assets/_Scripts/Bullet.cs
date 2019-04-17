using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : ColoredEntity
{
    public int damage;
    //public float yVelocity;

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
        if (collision.gameObject.GetComponent<DamagableEntity>() != null)
        {

            collision.gameObject.GetComponent<DamagableEntity>().TakeDamage(damage, curColor);
            Destroy(this.gameObject);
        }

        if (collision.gameObject.GetComponent<Bullet>() != null)
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
