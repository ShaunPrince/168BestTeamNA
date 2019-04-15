using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public float yVelocity;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Rigidbody>().velocity = new Vector3(0, yVelocity, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int GetDamage()
    {
        return damage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<DamagableEntity>() != null)
        {
            collision.gameObject.GetComponent<DamagableEntity>().TakeDamage(damage);
            Destroy(this.gameObject);
        }

        if (collision.gameObject.GetComponent<Bullet>() != null)
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
