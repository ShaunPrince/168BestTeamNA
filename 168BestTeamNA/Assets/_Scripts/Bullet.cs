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
        if (!gotColor)
        {
            int colorInt = GameObject.FindWithTag("Player").GetComponent<PlayerManager>().GetPlayerColorInt();
            playerColor = (ColoredEntity.EColor)colorInt;
            Debug.Log("Set bullet color to: " + playerColor);
            //ReColor(playerColor);
            gotColor = true;
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<DamagableEntity>() != null)
        {
            
            collision.gameObject.GetComponent<DamagableEntity>().TakeDamage(damage, playerColor);
            Destroy(this.gameObject);
        }

        if (collision.gameObject.GetComponent<Bullet>() != null)
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }

        if (!collision.collider.tag.Equals("Projectile"))
        {
            Destroy(this.gameObject);
        }
    }
}
