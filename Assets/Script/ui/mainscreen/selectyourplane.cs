using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class selectyourplane : MonoBehaviour
{

    public GameObject[] cameras;
    private int currentCameraIndex;
    Slider[] sliders;
    private void Start()
    {
        sliders=GameObject.Find("Canvas").GetComponentsInChildren<Slider>();
        if (cameras.Length > 0)
        {
            for (int i = 1; i < cameras.Length; i++)
            {
                cameras[i].gameObject.SetActive(false);
            }
            cameras[0].gameObject.SetActive(true);
            displayinfo();
            currentCameraIndex = 0;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            nextplane();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.UpArrow)) {
            previousplane();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow)) {
            nextplane();
        }
        if (Input.GetKeyDown(KeyCode.Return)) {
            selectthisplane();
        }

    }
    public void nextplane()
    {
        cameras[currentCameraIndex].gameObject.SetActive(false);
        currentCameraIndex++;
        if (currentCameraIndex >= cameras.Length)
        {
            currentCameraIndex = 0;
        }
        cameras[currentCameraIndex].gameObject.SetActive(true);
        displayinfo();
    }
    public void previousplane()
    {
        cameras[currentCameraIndex].gameObject.SetActive(false);
        currentCameraIndex--;
        if (currentCameraIndex <0)
        {
            currentCameraIndex = cameras.Length-1;
        }
        cameras[currentCameraIndex].gameObject.SetActive(true);
        displayinfo();
    }
    public void displayinfo()
    {
        sliders[0].value = GameSetting.plane_info[currentCameraIndex]["Health"]/GameSetting.maxHealth;
        sliders[1].value = GameSetting.plane_info[currentCameraIndex]["Damage"] / GameSetting.maxDamage;
        sliders[2].value = GameSetting.plane_info[currentCameraIndex]["Speed"] / GameSetting.maxSpeed;
    }
   public void selectthisplane()
    {
        GameSetting.currentplayerplane = currentCameraIndex;
        print("you have selected starShip: " + GameSetting.currentplayerplane);
        //change scene here
        SceneManager.LoadScene("TeamProject");
    }
}
