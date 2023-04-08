using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OurSpaceshipcontroller : MonoBehaviour
{
    Rigidbody playerspaceship;

    float verticalMove;
    float horizontalMove;
    float mouseInputX;
    float mouseInputY;
    float rollinput;

    Attributes playerattriutes;

    private void Start()
    {
        
        playerspaceship = transform.GetComponent<Rigidbody>();
    }
    private void Update()
    {
        verticalMove = Input.GetAxis("Vertical");
        horizontalMove = Input.GetAxis("Horizontal");
        rollinput = Input.GetAxis("Roll");

        mouseInputX = Input.GetAxis("Sideways");
    }
    private void FixedUpdate()
    {
        print(verticalMove);
        playerattriutes = transform.GetComponent<player>().ResultAttr;
        playerspaceship.AddForce(playerspaceship.transform.TransformDirection(Vector3.forward) * verticalMove * playerattriutes.movespeed*-1*Time.deltaTime, ForceMode.VelocityChange);
        playerspaceship.AddForce(playerspaceship.transform.TransformDirection(Vector3.right) * horizontalMove * playerattriutes.movespeed * Time.deltaTime, ForceMode.VelocityChange);
        playerspaceship.AddTorque(playerspaceship.transform.right*playerattriutes.turnrate*mouseInputX*-1 * Time.deltaTime, ForceMode.VelocityChange);
        playerspaceship.AddTorque(playerspaceship.transform.up * playerattriutes.turnrate *mouseInputY * Time.deltaTime, ForceMode.VelocityChange);
        playerspaceship.AddTorque(playerspaceship.transform.forward * rollinput * Time.deltaTime, ForceMode.VelocityChange);

    }
}
