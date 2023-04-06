using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] asteroids;

    // Asteroids will fly towards the center of the map
    public Transform mapCenter;
    //public float offset;

    public float maxScale = 5f;
    public float minScale = 1f;

    float time = 0.0f;
    public float spawnInterval = 3f;

    void Start()
    {

    }


    void Update()
    {
        time += Time.deltaTime;

        if (time >= spawnInterval)
        {
            time = time - spawnInterval;
            InstantiateRandomAsteroid();
        }

    }

    void InstantiateRandomAsteroid()
    {
        GameObject asteroid = Instantiate(asteroids[Random.Range(0, asteroids.Length)], spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.Euler(0, 0, 0));

        asteroid.transform.LookAt(mapCenter.position);

        float scale = Random.Range(minScale, maxScale);
        asteroid.transform.localScale = new Vector3(scale, scale, scale);
    }
}
