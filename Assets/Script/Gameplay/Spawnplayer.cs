using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnplayer : MonoBehaviour
{
    public GameObject[] player;
    public GameObject parent;
    private void Awake()
    {
        Instantiate(player[GameSetting.currentplayerplane],parent.transform);
    }
}
