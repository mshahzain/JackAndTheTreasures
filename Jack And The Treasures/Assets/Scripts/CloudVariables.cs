using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudVariables : MonoBehaviour
{
    public static int Highscore { get; set; }
    public static int Level { get; set; }
    public static int[] ImportantValues { get; set; }
    
    // Start is called before the first frame update
    void Awake()
    {
        ImportantValues = new int[3];

    }

    // Update is called once per frame
    void Update()
    {
       
        
    }
}
