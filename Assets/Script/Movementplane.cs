using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movementplane : MonoBehaviour
{

    public float amp;
    public float freq;
    public Transform platform;
    Vector3 initPos;
    
    void Start()
    {
        initPos = platform.position;
    }

    
    void Update()
    {
        transform.position = new Vector3(initPos.x, Mathf.Sin(Time.time * freq) * amp + initPos.y, initPos.z);
    }
}
