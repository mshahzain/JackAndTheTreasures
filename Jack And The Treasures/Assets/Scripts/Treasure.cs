﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Treasure : MonoBehaviour {
    [SerializeField] GameObject canvas;
    [SerializeField] Player player;
    //[SerializeField] Button button;
    //[SerializeField] Canvas canvas1;

    // Use this for initialization
    Animator animator;
    public bool Opened;
    public GameObject canvas1;
    public GameObject canvas2;
    public GameObject canvas3;
    public GameObject canvas4;
    public GameObject canvas5;
    public GameObject canvas6;
    public GameObject canvas7;
    public GameObject canvas8;
    public GameObject canvas9;
    public GameObject canvas10;
    public GameObject canvas11;
    public GameObject canvas12;
    public GameObject canvas13;



    private void Awake()
    {
        canvas.SetActive(false);
    }
    void Start () {
        canvas.SetActive(false);
        string  scenename = SceneManager.GetActiveScene().name;
        if (scenename == "Level1")
        {
            int randomizer = Random.Range(0, 3);
            if (randomizer == 0) canvas = canvas1;
            else if (randomizer == 1) canvas = canvas2;
            else if (randomizer == 2) canvas = canvas3;
            else if (randomizer == 3) canvas = canvas4;
        }
        else if (scenename == "Level1.1")
        {
            int randomizer = Random.Range(0, 12);
            if (randomizer == 0) canvas = canvas1;
            else if (randomizer == 1) canvas = canvas2;
            else if (randomizer == 2) canvas = canvas3;
            else if (randomizer == 3) canvas = canvas4;
            else if (randomizer == 4) canvas = canvas5;
            else if (randomizer == 5) canvas = canvas6;
            else if (randomizer == 6) canvas = canvas7;
            else if (randomizer == 7) canvas = canvas8;
            else if (randomizer == 8) canvas = canvas9;
            else if (randomizer == 9) canvas = canvas10;
            else if (randomizer == 10) canvas = canvas11;
            else if (randomizer == 11) canvas = canvas12;
            else if (randomizer == 12) canvas = canvas13;

        }
        else if (scenename == "Level1.2")
        {
            int randomizer = Random.Range(0, 9);
            if (randomizer == 0) canvas = canvas1;
            else if (randomizer == 1) canvas = canvas2;
            else if (randomizer == 2) canvas = canvas3;
            else if (randomizer == 3) canvas = canvas4;
            else if (randomizer == 4) canvas = canvas5;
            else if (randomizer == 5) canvas = canvas6;
            else if (randomizer == 6) canvas = canvas7;
            else if (randomizer == 7) canvas = canvas8;
            else if (randomizer == 8) canvas = canvas9;
            else if (randomizer == 9) canvas = canvas10;
        }
        else if (scenename == "Level1.3")
        {
            int randomizer = Random.Range(0, 3);
            if (randomizer == 0) canvas = canvas1;
            else if (randomizer == 1) canvas = canvas2;
            else if (randomizer == 2) canvas = canvas3;
            else if (randomizer == 3) canvas = canvas4;
        }
        player = FindObjectOfType<Player>();
        animator = GetComponent<Animator>();
        

        Opened = false;
    }
	
	// Update is called once per frame
	void Update (){
        player = FindObjectOfType<Player>();
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Opened == false)
        {
            Opened = true;
            animator.SetBool("Openit", true);
            canvas.SetActive(true);
            player.isCanvasOpened = true;
            //button.interactable = false;
            Time.timeScale = 0.0f;
        }
    }
}
