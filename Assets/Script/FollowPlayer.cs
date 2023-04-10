using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    private Vector3 position;

    // Update is called once per frame
    void Update() {
        position = player.transform.position;
        transform.position = position + Vector3.up * 300f;
        transform.rotation = Quaternion.LookRotation(-Vector3.up, player.transform.forward);
    }
}
