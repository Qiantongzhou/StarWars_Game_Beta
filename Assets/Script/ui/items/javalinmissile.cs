using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class javalinmissile : EnhancementSkill
{
    public override string Name => "javalinmissile";
    public override string Description => "lunchattack";
    public AudioClip clip;
    //AudioSource source;
    public GameObject missileprefab;
    bool startcount = false;
    //float count = 0;
    // Start is called before the first frame update
    public override void DoAction()
    {
      if(!startcount)
        {
            StartCoroutine(firemissile());
        }
    }
    IEnumerator firemissile()
    {
        startcount = true;
        GameObject player = GameObject.FindWithTag("Player");

        GameObject temp = Instantiate(missileprefab, player.transform.GetChild(1).transform.position, Quaternion.identity);
        temp.transform.localScale = temp.transform.localScale * 10;
        temp.transform.localRotation = player.transform.GetChild(1).transform.rotation;
        yield return new WaitForSeconds(1);
        startcount = false;
    }
      
}
