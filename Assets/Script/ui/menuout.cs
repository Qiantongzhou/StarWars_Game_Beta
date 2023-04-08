using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuout : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GameObject.Find("Canvas").transform.GetChild(0).GetComponent<Animator>();
    }

    public void menuoutarrow()
    {
        transform.localScale = new Vector3(-transform.localScale.x,transform.localScale.y,transform.localScale.z);
        if(animator != null )
        {
            animator.SetFloat("multi", -animator.GetFloat("multi"));
            animator.SetBool("out", true);
            print(-animator.GetFloat("multi"));
            
        }
    }
}
