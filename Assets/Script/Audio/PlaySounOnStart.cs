using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySounOnStart : MonoBehaviour
{
    [SerializeField] private AudioClip _clip;

    void Start()
    {
        SoundManager.instance.PlaySound(_clip);
    }

}
