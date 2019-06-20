using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMover : MonoBehaviour
{
    public float SpinSpeed = 180f;
    public float motionMagnitude = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(Vector3.up * SpinSpeed * Time.deltaTime);
        if (gameObject.transform.up.y >= 0)
            gameObject.transform.Translate(Vector3.up * (Mathf.Sin(Time.timeSinceLevelLoad)) * motionMagnitude);
    }
}
