﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject.Find("Health").GetComponent<Text>().text = GameObject.Find("Floor").GetComponent<DamagableEntity>().health.ToString(); 
    }
}
