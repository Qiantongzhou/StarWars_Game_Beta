using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private Transform _ship;
    public Transform _destination;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")|| other.CompareTag("Team1") || other.CompareTag("Team2"))
        {
            _ship = other.transform;
            teleport();
        }
        else {
            // Todo
        }
    }

    public void teleport() {
        Vector3 portalUp = _destination.transform.up;
        Vector3 shipForward = _ship.transform.forward;
        float angle = Vector3.SignedAngle(shipForward, portalUp, Vector3.Cross(portalUp, shipForward));

        _ship.Rotate(Vector3.Cross(portalUp, shipForward), angle);
        _ship.position = _destination.position + _destination.up * 60f;
    }

}
