using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRoutin : MonoBehaviour
{
    [SerializeField] float distanceToCover;
    [SerializeField] float speed;

    private Vector3 startingPosition;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v = startingPosition;
        v.x += distanceToCover * Mathf.Sin(Time.time * speed);
        transform.position = v;
    }
}
