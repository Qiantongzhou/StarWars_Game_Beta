using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthbarworldspace : MonoBehaviour
{
    public Image healthbar;
    // Start is called before the first frame update
    public void updatehealth(float max,float current)
    {
        healthbar.fillAmount=current/max;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - Camera.allCameras[0].transform.position);
    }
}
