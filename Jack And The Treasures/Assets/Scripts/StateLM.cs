using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "StateLM")]
public class StateLM : ScriptableObject
{

    [SerializeField] Sprite sprites;
    [SerializeField] StateLM[] nextStates;
    [TextArea(14, 10)] [SerializeField] string textComponent;

    public string GetText()
    {
        return textComponent;
    }

    public Sprite GetStateSprite()
    {
        return sprites;
    }

    public StateLM[] GetNextStates()
    {
        return nextStates;
    }
}
