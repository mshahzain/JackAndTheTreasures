using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonManager : MonoBehaviour {
    public bool isCorrect = false;
    [SerializeField] GameObject canvas;
    [SerializeField] Player player;
    //[SerializeField] Button button;
    
	// Use this for initialization
	void Start () {
        //obj = FindObjectOfType<GameObject>;
        
            player = FindObjectOfType<Player>();
	}
	
	// Update is called once per frame
	void Update () {

        
            player = FindObjectOfType<Player>();

    }

    public void Check()
    {
        if (isCorrect == true)
        {
            canvas.SetActive(false);
            Time.timeScale = 1f;
            player.isCanvasOpened = false;
            //button.interactable = true;
            
        }
        else
        {
            Debug.Log("NOOOO");
        }

    }

    

}
