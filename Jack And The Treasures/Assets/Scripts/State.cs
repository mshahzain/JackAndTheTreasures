using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State")]
public class State : ScriptableObject
{

    [TextArea(10,14)] [SerializeField] string Question;
    [TextArea] [SerializeField] string[] Options;
    public int correctOption;
    



    [SerializeField] State nextState;
   // [SerializeField] State previousState;

    public string GetStateQuestion()
    {
        return Question;
    }

    public string[] GetStateOptions()
    {
        return Options;
    }

    public State GetNextState()
    {
        
        return nextState;
    }
   /* public State GetPreviousState()
    {
        return previousState;
    }*/
}
