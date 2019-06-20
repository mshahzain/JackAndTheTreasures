using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUnlock : MonoBehaviour
{
    [SerializeField] Button [] buttons;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int levelsUnlocked = CloudVariables.ImportantValues[1];
        for (int i = 0; i < levelsUnlocked + 1; i++)
            buttons[i].interactable = true;

    }
}
