using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int moveSpeed;
    public Transform bulletPrefab;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayerState();
    }

    //Update, or "main", method to be used by server
    public void UpdatePlayerState()
    {
        UpdateMovement();
        if (Input.GetKeyDown("space"))
            Shoot();
    }

    private void UpdateMovement()
    {
        float mov_x = Input.GetAxisRaw("Horizontal");
        rb.AddForce(new Vector3(mov_x * moveSpeed, 0.0f, 0.0f));
    }

    private void Shoot()
    {
        Transform bullet = GameObject.Instantiate(bulletPrefab, this.transform.position + Vector3.up, Quaternion.identity);
    }
}
