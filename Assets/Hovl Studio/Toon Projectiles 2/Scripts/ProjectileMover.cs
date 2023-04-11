using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMover : MonoBehaviour
{
    public float speed = 120f;
    public float hitOffset = 0f;
    public bool UseFirePointRotation;
    public Vector3 rotationOffset = new Vector3(0, 0, 0);
    public GameObject hit;
    public GameObject flash;
    private Rigidbody rb;
    public GameObject[] Detached;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (flash != null)
        {
            var flashInstance = Instantiate(flash, transform.position, Quaternion.identity);
            flashInstance.transform.forward = gameObject.transform.forward;
            var flashPs = flashInstance.GetComponent<ParticleSystem>();
            if (flashPs != null)
            {
                Destroy(flashInstance, flashPs.main.duration);
            }
            else
            {
                var flashPsParts = flashInstance.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(flashInstance, flashPs.main.duration);
            }
        }
        Destroy(gameObject,10);
	}

    void FixedUpdate ()
    {
        if (speed != 0) {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 100f);
            Transform target = null;
            float minDistance = Mathf.Infinity;
            foreach (var hitCollider in hitColliders) {
                if (hitCollider.gameObject.CompareTag("Team2")) {
                    float distance = Vector3.Distance(transform.position, hitCollider.gameObject.transform.position);
                    if (distance < minDistance) {
                        target = hitCollider.gameObject.transform;
                        minDistance = distance;
                    }
                }
            }
            if (target == null) {
                rb.velocity = transform.forward * speed;
            }
            else {
                Vector3 targetDir = target.position - transform.position;
                Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, 10 * Time.fixedDeltaTime, 0f);
                transform.rotation = Quaternion.LookRotation(newDir);
                rb.velocity = transform.forward * speed;
            }
        }
    }

    //https ://docs.unity3d.com/ScriptReference/Rigidbody.OnCollisionEnter.html
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            return;
        }
        //Lock all axes movement and rotation
        rb.constraints = RigidbodyConstraints.FreezeAll;
        speed = 0;

        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point + contact.normal * hitOffset;

        if (hit != null)
        {
            var hitInstance = Instantiate(hit, pos, rot);
            if (UseFirePointRotation) { hitInstance.transform.rotation = gameObject.transform.rotation * Quaternion.Euler(0, 180f, 0); }
            else if (rotationOffset != Vector3.zero) { hitInstance.transform.rotation = Quaternion.Euler(rotationOffset); }
            else { hitInstance.transform.LookAt(contact.point + contact.normal); }

            var hitPs = hitInstance.GetComponent<ParticleSystem>();
            if (hitPs != null)
            {
                Destroy(hitInstance, hitPs.main.duration);
            }
            else
            {
                var hitPsParts = hitInstance.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(hitInstance, hitPsParts.main.duration);
            }
        }
        foreach (var detachedPrefab in Detached)
        {
            if (detachedPrefab != null)
            {
                detachedPrefab.transform.parent = null;
            }
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Missile")) {
            return;
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Agent")) {
            if (other.tag != "Player") {
                // Warning: This code is just for demo;
                if (other.tag == "Team2") {
                    if (other.name == "MotherShip") {
                        other.GetComponent<mothershiphealth>().takedamage(100);
                    }
                    else {
                        other.GetComponent<AgentHealth>().takedamage(100);
                    }
                }
                Destroy(gameObject);
            }
        }
    }
}
