using UnityEngine;

public class Rotator : MonoBehaviour
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
