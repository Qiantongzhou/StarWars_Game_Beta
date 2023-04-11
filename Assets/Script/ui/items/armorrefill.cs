using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armorrefill : EnhancementSkill
{
    public override string Name => "refill";
    public override string Description => "refillarmor";
    public AudioClip clip;
    AudioSource source;
    bool startcount = false;
    float count = 0;

    public override void DoAction()
    {
        player temp = GameObject.FindGameObjectWithTag("Player").GetComponent<player>();
        temp.setcurrentmagic(temp.ResultAttr.armor);
        Destroy(gameObject);
    }
   
}

