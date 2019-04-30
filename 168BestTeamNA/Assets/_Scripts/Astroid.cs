using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Astroid : DamagableEntity
{
    [SyncVar] public int damage;

    [SyncVar] public int scaleFactor;

    public Renderer glowRock;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ReColorAsteroid(curColor, glowRock);
        this.transform.localScale = new Vector3(1,1,1) * scaleFactor;

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
