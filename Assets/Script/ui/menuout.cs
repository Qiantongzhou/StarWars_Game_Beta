using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuout : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GameObject.Find("Canvas").transform.GetChild(GameObject.Find("Canvas").transform.childCount-1).GetComponent<Animator>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            menuoutarrow();
        }
    }

    public void menuoutarrow()
    {
        transform.localScale = new Vector3(-transform.localScale.x,transform.localScale.y,transform.localScale.z);
        if(animator != null )
        {
            animator.SetBool("menuOutTrigger", true);
        }
    }
}
