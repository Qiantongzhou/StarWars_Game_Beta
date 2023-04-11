using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class switchcamera : MonoBehaviour
{
    public GameObject[] cameras;
    public GameObject[] dialogs;
    private int currentCameraIndex;
    // Start is called before the first frame update
    void Start()
    {
        if (cameras.Length > 0)
        {
            for (int i = 1; i < cameras.Length; i++)
            {
                cameras[i].gameObject.SetActive(false);
            }
            cameras[0].gameObject.SetActive(true);
            currentCameraIndex = 0;
            StartCoroutine(cinematic());
        }
        foreach(GameObject dia in dialogs) {
        dia.SetActive(false );
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator cinematic()
    {
        yield return new WaitForSeconds(0.2f);
        dialogs[0].SetActive(true);
        yield return new WaitForSeconds(3f);
        dialogs[0].SetActive(false);
        nextcam();
        yield return new WaitForSeconds(2f);
        
        dialogs[1].SetActive(true);
        yield return new WaitForSeconds(5f);
        nextcam();
        dialogs[1].SetActive(false);
        yield return new WaitForSeconds(1f);
        
        dialogs[2].SetActive(true);
        yield return new WaitForSeconds(5f);
        nextcam();
        dialogs[2].SetActive(false);
        yield return new WaitForSeconds(3f);
        nextscene();
    }
    public void nextcam()
    {
        cameras[currentCameraIndex].gameObject.SetActive(false);
        currentCameraIndex++;
        if (currentCameraIndex >= cameras.Length)
        {
            currentCameraIndex = 0;
        }
        cameras[currentCameraIndex].gameObject.SetActive(true);
        
    }
    public void previouscam()
    {
        cameras[currentCameraIndex].gameObject.SetActive(false);
        currentCameraIndex--;
        if (currentCameraIndex < 0)
        {
            currentCameraIndex = cameras.Length - 1;
        }
        cameras[currentCameraIndex].gameObject.SetActive(true);
        
    }
    public void nextscene()
    {
        SceneManager.LoadScene("TeamProject");
    }
}
