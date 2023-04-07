using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Missle : MonoBehaviour
{
    private float speed = 100f;
    private float rotationSpeed = 15f;
    public Transform target;
    public GameObject effectPrefab;

    private Rigidbody rb;
    private Vector3 forwardDirection;
    private float timer = 0f;
    private float forwardTime = 0.1f;
    private float stopDistance = 30f;
    private bool isTargetLost = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
        forwardDirection = transform.forward;
        Invoke("StartTracking", 0f);
    }

    private void StartTracking() {
        InvokeRepeating("TrackTarget", 0f, 0.1f + Random.Range(0f, 0.1f));
        Invoke("DestroyMissile", 15f);
    }

    private void TrackTarget() {
        timer += Time.fixedDeltaTime;
        Vector3 direction = (target.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, target.position);
        if (timer < forwardTime) {
            direction = forwardDirection;
        }

        if(distance < stopDistance || isTargetLost) {
            rb.velocity = transform.forward * speed;
            isTargetLost = true;
        }
        else {
            Vector3 rotation = Vector3.RotateTowards(transform.forward, direction, rotationSpeed * Time.deltaTime, 0f);
            transform.rotation = Quaternion.LookRotation(rotation);
            rb.velocity = transform.forward * speed;
        }
    }

    private void DestroyMissile() {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision) {
        Instantiate(effectPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
