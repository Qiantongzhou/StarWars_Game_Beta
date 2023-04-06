using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public float speed = 100f;

    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * speed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MapLimit"))
        {
            // Instantiate Explosion
            Destroy(gameObject);
        }
    }
}
