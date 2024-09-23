using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationEnemy : MonoBehaviour
{
    [SerializeField] private float x, y, z;
    [SerializeField] private float speed = 1;
    // Update is called once per frame
    void Update()
    {
        // Rotate the object on X, Y, and Z axes by specified amounts, adjusted for frame rate.
        transform.Rotate(new Vector3(x, y, z) * Time.deltaTime * speed);
    }
}
