using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int moveSpeed;
    public Transform bulletPrefab;

    private Rigidbody rb;
    private int bulletSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        bulletSpeed = 20;
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
        if (Input.GetMouseButtonDown(0))
            Shoot();
    }

    private void UpdateMovement()
    {
        float mov_x = Input.GetAxisRaw("Horizontal");
        rb.AddForce(new Vector3(mov_x * moveSpeed, 0.0f, 0.0f));
    }

    private void Shoot()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Mathf.Abs(Camera.main.transform.position.z));
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Debug.Log(mousePos);
        //Vector3 bulletVelocity = GetVelocity(mousePos);
        Vector3 targetPos = mousePos - this.transform.position;
        GameObject bullet = GameObject.Instantiate(bulletPrefab, this.transform.position + Vector3.up, Quaternion.LookRotation(targetPos, Vector3.up)).gameObject;
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.velocity = bullet.transform.forward * bulletSpeed;
    }

    private Vector3 GetVelocity(Vector3 mousePos)
    {
        Vector3 pos = this.transform.position;
        //float angle = Vector3.Angle(pos, mousePos);
        float angle = Mathf.Atan2(mousePos.y - pos.y, mousePos.x - pos.x) * 180 / Mathf.PI;
        float x = bulletSpeed * Mathf.Cos(angle);
        float y = bulletSpeed * Mathf.Sin(angle);
        //float c = Vector3.Distance(this.transform.position, mousePos);
        return new Vector3 (x, y, 0.0f);
    }
}
