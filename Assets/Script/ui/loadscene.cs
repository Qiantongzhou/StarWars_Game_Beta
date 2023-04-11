using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadscene : MonoBehaviour
{
    public void Loadscene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
