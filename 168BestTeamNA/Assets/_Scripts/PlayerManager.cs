using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class PlayerManager : NetworkBehaviour
{
    public int moveSpeed;

    public Camera myCam;

    private Rigidbody m_Rigidbody;

    public Rigidbody Rigidbody
    {
        get
        {
            return m_Rigidbody;
        }
    }

    void Start()
    {
        m_Rigidbody = this.GetComponent<Rigidbody>();

    }

    void Update()
    {
        if (!isLocalPlayer)
        {
            // exit from update if this is not the local player
            // hopefully this fixes the issue with controlling all players
            return;
        }

        UpdatePlayerState();
        myCam.transform.position = new Vector3(this.transform.position.x, myCam.transform.position.y, myCam.transform.position.z);
    }

    public void UpdatePlayerState()
    {
        UpdateMovement();
        if (Input.GetMouseButtonDown(0)) return;
            //Shoot();
    }

    private void UpdateMovement()
    {
        float mov_x = Input.GetAxisRaw("Horizontal");
        m_Rigidbody.AddForce(new Vector3(mov_x * moveSpeed, 0.0f, 0.0f));
    }
}
