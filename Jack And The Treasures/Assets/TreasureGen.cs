﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureGen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "treasure")
        {
            //If the GameObject's name matches the one you suggest, output this message in the console
            Debug.Log("Nice NIice");
        }


    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
