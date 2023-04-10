using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSpawner : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public List<Transform> spawnPoints;

    private List<GameObject> spawnedObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform t in spawnPoints)
        {
            SpawnShip(t);
        }
    }

    void SpawnShip(Transform spawnpoint)
    {
        
        GameObject spawnedObject = Instantiate(prefabToSpawn, spawnpoint.position, spawnpoint.rotation);
        spawnedObjects.Add(spawnedObject);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = spawnedObjects.Count - 1; i >= 0; i--)
        {
            if (spawnedObjects[i] == null)
            {
                spawnedObjects.RemoveAt(i);
                SpawnShip(spawnPoints[Random.Range(0, spawnPoints.Count)]);
            }
        }
    }
}
