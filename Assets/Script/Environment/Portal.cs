using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public static Portal instance;
    private Transform _ship;
    public Transform _destination;

<<<<<<< Updated upstream
    private static bool playerIsOverlapping = false;

    private void Start()
    {
       
    }
    private void LateUpdate()
    {
        //Vector3 portalToPlayer = _ship.position - transform.position;
        // Teleport ship
        if (playerIsOverlapping)
        {
            float rotationDif = -Quaternion.Angle(transform.rotation, _destination.rotation);
            rotationDif += 180;

            _ship.Rotate(Vector3.up, rotationDif);

            //Vector3 positonOffset = Quaternion.Euler(0f, rotationDif, 0f) * portalToPlayer;
            //_ship.position = _destination.position + positonOffset;
            _ship.position = _destination.position;

            StartCoroutine(Teleport());
        }
    }

    IEnumerator Teleport()
    {
        yield return new WaitForSeconds(4);
        playerIsOverlapping = false;
=======
    public AudioSource _source;

    private void Start()
    {
        _source = GetComponent<AudioSource>();
>>>>>>> Stashed changes
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

<<<<<<< Updated upstream
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Enemy") || other.CompareTag("Ally"))
        {
           
        }
=======
    public void teleport() {

        if(_source != null)
            _source.Play();

        Vector3 portalUp = _destination.transform.up;
        Vector3 shipForward = _ship.transform.forward;
        float angle = Vector3.SignedAngle(shipForward, portalUp, Vector3.Cross(portalUp, shipForward));

        _ship.Rotate(Vector3.Cross(portalUp, shipForward), angle);
        _ship.position = _destination.position + _destination.up * 60f;
>>>>>>> Stashed changes
    }
}
