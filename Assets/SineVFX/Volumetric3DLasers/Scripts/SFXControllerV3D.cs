using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SFXControllerV3D : MonoBehaviour
{

    public AudioSource loopingSFX;
    public GameObject[] waveSfxPrefabs;
    public bool isFired = false;

    private float globalProgress;

    public void SetGlobalProgress(float gp)
    {
        globalProgress = gp;
    }

    void Update()
    {
        if (isFired)
        {
            Instantiate(waveSfxPrefabs[Random.Range(0, waveSfxPrefabs.Length)], transform.position, transform.rotation);
            isFired = false;
        }

        loopingSFX.volume = globalProgress;
    }



}
