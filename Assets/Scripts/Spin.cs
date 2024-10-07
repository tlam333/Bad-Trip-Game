using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    // Rotation speed in degrees per second
    public Vector3 rotationSpeed = new Vector3(0, 100, 0);

    // Update is called once per frame
    void Update()
    {
        // Rotate the object based on the rotationSpeed value
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}

