/*using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdventureGame : MonoBehaviour
{

    [SerializeField] Text[] textComponent;
    [SerializeField] State startingState;
    public ButtonManager[] buttons;
   

    State state;

	// Use this for initialization
	void Start()
    {
        state = startingState;
        ManagingStates();

    }

    // Update is called once per frame
    void Update()
    {
        //ManageState();
	}

    /*private void ManageState()
    {
        var nextStates = state.GetNextState();
        for (int index = 0; index < nextStates.Length; index++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                state = nextStates;
            }
        //}
       // ManagingStates();
    
    void ManagingStates()
    {
        textComponent[0].text = state.GetStateQuestion();
        textComponent[1].text = state.GetStateOptions()[0];
        textComponent[2].text = state.GetStateOptions()[1];
        textComponent[3].text = state.GetStateOptions()[2];
        textComponent[4].text = state.GetStateOptions()[3];
        buttons[state.correctOption].isCorrect = true;
    }

    public void Clicked()
    {
        Debug.Log("Button Clicked");
        var nextStates = state.GetNextState();
        state = nextStates;
        ManagingStates();

    }

}
*/