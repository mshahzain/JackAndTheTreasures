﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canvasClose : MonoBehaviour
{
    public GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Close()
    {
        canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
