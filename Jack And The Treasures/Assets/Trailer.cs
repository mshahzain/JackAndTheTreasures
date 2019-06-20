using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trailer : MonoBehaviour
{

    [SerializeField] Camera maincamera;
    Vector3 position;
    [SerializeField] float speed=1f;
    // Start is called before the first frame update
    void Start()
    {
        position = maincamera.transform.position;
        //position.z = 100f;
        //maincamera.transform.position = position;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        float temp2 = position.z - speed;
        if (temp2 > - 13f)
            position.z = temp2;
        maincamera.transform.position = position;
    }
}
