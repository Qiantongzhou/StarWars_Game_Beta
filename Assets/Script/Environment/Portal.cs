using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public static Portal instance;
    public Transform _ship;
    public Transform _destination;

    private static bool playerIsOverlapping = false;

    private void Start()
    {
       
    }
    private void LateUpdate()
    {
        Vector3 portalToPlayer = _ship.position - transform.position;
        // Teleport ship
        if (playerIsOverlapping)
        {
            float rotationDif = -Quaternion.Angle(transform.rotation, _destination.rotation);
            rotationDif += 180;

            _ship.Rotate(Vector3.up, rotationDif);

            Vector3 positonOffset = Quaternion.Euler(0f, rotationDif, 0f) * portalToPlayer;
            _ship.position = _destination.position + positonOffset;
            StartCoroutine(Teleport());
        }
    }

    IEnumerator Teleport()
    {
        yield return new WaitForSeconds(4);
        playerIsOverlapping = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Enemy") || other.CompareTag("Ally"))
        {
            _ship = other.transform;
            playerIsOverlapping = true;
            //StartCoroutine(Teleport());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Enemy") || other.CompareTag("Ally"))
        {
           
        }
    }
}
