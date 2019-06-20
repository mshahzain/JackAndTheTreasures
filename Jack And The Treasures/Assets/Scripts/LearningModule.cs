using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LearningModule : MonoBehaviour
{

    [SerializeField] Image m_sprite;
    [SerializeField] Text textComponent;
    [SerializeField] StateLM startingState;

    StateLM state;

    // Use this for initialization
    void Start()
    {
        state = startingState;
        m_sprite.sprite = state.GetStateSprite();
        textComponent.text = state.GetText();
    }

    // Update is called once per frame
    void Update()
    {
        ManageState();
    }

    private void ManageState()
    {
        var states = state.GetNextStates();
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            state = states[1];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            state = states[0];
        }

        m_sprite.sprite = state.GetStateSprite();
        textComponent.text = state.GetText();

    }

    public void NextState()
    {
        var states = state.GetNextStates();
        state = states[1];
        m_sprite.sprite = state.GetStateSprite();
        textComponent.text = state.GetText();
    }

    public void PreviousState()
    {
        var states = state.GetNextStates();
        state = states[0];
        m_sprite.sprite = state.GetStateSprite();
        textComponent.text = state.GetText();
    }
}
