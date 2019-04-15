using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagableEntity : MonoBehaviour
{
    //EColor will be used for identification and damage
    public enum EColor {Red, Blue, Green, Yellow, Gray};

    public EColor curColor;
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
        Bullet incomingBullet = collision.gameObject.GetComponent<Bullet>();
        if (incomingBullet != null)
        {
            this.TakeDamage(incomingBullet.damage);
            Destroy(incomingBullet.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public virtual void TakeDamage(float dmg)
    {
        this.health -= dmg;
        if (this.health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
