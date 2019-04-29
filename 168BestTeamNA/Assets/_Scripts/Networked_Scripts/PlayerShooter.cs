using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerShooter : NetworkBehaviour
{
    public GameObject bulletPrefab;
    public int bulletSpeed;
    private Vector3 rightVectorLimit;
    private Vector3 leftVectorLimit;

    private bool gotColor = false;
    //private Material playerColor;
    private ColoredEntity.EColor playerColor;

    void Start()
    {
        rightVectorLimit = new Vector3(1.0f, 2.0f, 0.0f);
        leftVectorLimit = new Vector3(-1.0f, 2.0f, 0.0f);
    }

    private void Update()
    {
        if (!gotColor)
        {
            //playerColor = this.gameObject.GetComponent<PlayerManager>().GetPlayerColor();
            int colorInt = GameObject.FindWithTag("Player").GetComponent<PlayerManager>().GetPlayerColorInt();
            playerColor = (ColoredEntity.EColor)colorInt;
            gotColor = true;
        }

        if (isLocalPlayer)
        {
            if (Input.GetMouseButtonDown(0)) Shoot();
        }
    }

    public void Shoot()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Mathf.Abs(Camera.main.transform.position.z));
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3 targetPos = mousePos - this.transform.position;
        targetPos = CheckTargetPos(targetPos);
        //GameObject bullet = GameObject.Instantiate(bulletPrefab, this.transform.position + Vector3.up, Quaternion.LookRotation(targetPos, Vector3.up)).gameObject;
        //bullet.GetComponent<Bullet>().GetComponentInChildren<Renderer>().material = playerColor;
        //Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        //bulletRb.velocity = bullet.transform.forward * bulletSpeed;

        // spawn the bullet on the server
        CmdSpawnBullet(targetPos);

    }

    private Vector3 CheckTargetPos(Vector3 targetPos)
    {
        Vector3 newTargetPos;
        float angle = Vector3.Angle(targetPos, Vector3.up);
        if (angle > 45.0f)
        {
            if (targetPos.x > 0.0f)
                newTargetPos = rightVectorLimit;
            else
                newTargetPos = leftVectorLimit;
        }
        else
        {
            newTargetPos = targetPos;
        }
        return newTargetPos;
    }

    [Command]
    void CmdSpawnBullet(Vector3 targetPos)
    {
        GameObject bullet = GameObject.Instantiate(bulletPrefab, this.transform.position + Vector3.up, Quaternion.LookRotation(targetPos, Vector3.up)).gameObject;
        //bullet.GetComponent<Bullet>().GetComponentInChildren<Renderer>().material = playerColor;
        bullet.GetComponent<Bullet>().ReColor(playerColor);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.velocity = bullet.transform.forward * bulletSpeed;

        Debug.Log(this.gameObject);
        NetworkServer.Spawn(bullet);
        Debug.Log("Spawning bullet on server");
    }

}
