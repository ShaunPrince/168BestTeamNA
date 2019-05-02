using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    public GameObject gameOverUI;
    //public GameObject floor;
    // Start is called before the first frame update
    void Start()
    {
        gameOverUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject tempFloor = GameObject.Find("Floor");
        if (tempFloor == null)
        {
            gameOverUI.SetActive(true);
        }
    }
}
