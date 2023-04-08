using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public float speed = 100f;

    public GameObject explosionPrefab;


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
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            //explosion.GetComponent<ParticleSystem>().startSize = gameObject.transform.localScale.x;
            Destroy(gameObject);
        }
    }
}
