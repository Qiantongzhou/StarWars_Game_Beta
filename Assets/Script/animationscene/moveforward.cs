using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveforward : MonoBehaviour
{
    public float speed=70;
    void Update()
    {
        transform.position += Vector3.forward*speed*Time.deltaTime;
    }
}
