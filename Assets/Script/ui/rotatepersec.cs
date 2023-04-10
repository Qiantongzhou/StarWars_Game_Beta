using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatepersec : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(0, 0, 0); // Specify rotation speed in degrees per second for each axis

    private void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
