using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : ColoredEntity
{
    public int moveSpeed;
    public Transform bulletPrefab;
    public int bulletSpeed;
    public Vector3 vectorLimit;

    public Camera myCam;

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
        myCam.transform.position = new Vector3(this.transform.position.x, myCam.transform.position.y, myCam.transform.position.z);
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
        Vector3 targetPos = mousePos - this.transform.position;
        targetPos = CheckTargetPos(targetPos);
        GameObject bullet = GameObject.Instantiate(bulletPrefab, this.transform.position + Vector3.up, Quaternion.LookRotation(targetPos, Vector3.up)).gameObject;
        bullet.GetComponent<Bullet>().ReColor(curColor);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.velocity = bullet.transform.forward * bulletSpeed;
    }

    private Vector3 CheckTargetPos(Vector3 targetPos)
    {
        Vector3 newTargetPos;
        float angle = Vector3.Angle(targetPos, Vector3.up);
        if (angle > 45.0f)
        {
            if (targetPos.x > 0.0f)
                newTargetPos = vectorLimit;
            else
            {
                newTargetPos = vectorLimit;
                newTargetPos.x = newTargetPos.x * -1.0f;
            }
        }
        else
        {
            newTargetPos = targetPos;
        }
        return newTargetPos;
    }
}
