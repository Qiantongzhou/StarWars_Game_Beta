using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSpawner : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public List<Transform> spawnPoints;

    private List<GameObject> spawnedObjects = new List<GameObject>();
    private Dictionary<int, float> timers;

    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform t in spawnPoints)
        {
            SpawnShip(t);
        }
        timers = new Dictionary<int, float>();
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
            // if not activated
            if (!spawnedObjects[i].activeSelf) {
                // if not in timers, add timers
                if (!timers.ContainsKey(i)) {
                    timers[i] = Time.time + 20f;
                }
                // if in timers, check if time is up
                else if (Time.time >= timers[i]) {
                    spawnedObjects.RemoveAt(i);
                    SpawnShip(spawnPoints[Random.Range(0, spawnPoints.Count)]);
                    timers.Remove(i);
                }
            }
            // if activated, remove timers
            else {
                if (timers.ContainsKey(i)) {
                    timers.Remove(i);
                }
            }
        }
    }
}
