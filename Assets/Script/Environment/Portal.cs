using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private Transform _ship;
    public Transform _destination;

    public AudioSource _source;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _ship = other.transform;
            teleport();
        }
        else {
            // Todo
        }
    }

    public void teleport() {
        if(_source != null)
        {
            Debug.Log("Playing sound from " + _source.name);
            _source.Play();
        }

        Vector3 portalUp = _destination.transform.up;
        Vector3 shipForward = _ship.transform.forward;
        float angle = Vector3.SignedAngle(shipForward, portalUp, Vector3.Cross(portalUp, shipForward));

        _ship.Rotate(Vector3.Cross(portalUp, shipForward), angle);
        _ship.position = _destination.position + _destination.up * 60f;
    }

}
