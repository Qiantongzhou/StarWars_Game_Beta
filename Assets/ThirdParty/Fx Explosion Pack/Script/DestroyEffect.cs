using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class DestroyEffect : MonoBehaviour {
    private void Start () {
        Destroy(gameObject, 5f);
    }
}
