using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AgentHealth : MonoBehaviour
{
    Attributes agentattributes;
    public Canvas healthdisplay;
    private int currenthealth;
    private float lastCollisionTime;
    private float collisionCooldown = 2f;


    private int currentmagicpoint;


    public int damageblock { get; set; }

    public int attackdamage { get; set; }
    public int attackdamagebonus { get; set; }

    public int critdamage { get; set; }
    public int critchance { get; set; }

    public int magicdamage { get; set; }


    public int attackrange { get; set; }
    public int attackspeed { get; set; }

    public int movespeed { set; get; }
    private void Start()
    {
        agentattributes=transform.GetComponent<Attributes>();
        agentattributes.movespeed = Mathf.FloorToInt(GameSetting.agent_info[0]["Speed"]);
        agentattributes.attackdamage = Mathf.FloorToInt(GameSetting.agent_info[0]["Damage"]);
        agentattributes.healthpoint = Mathf.FloorToInt(GameSetting.agent_info[0]["Health"]);
        currenthealth = agentattributes.healthpoint;
    }
    private void FixedUpdate()
    {
        float value = (float)getcurrenthealth() / agentattributes.healthpoint;
        Slider[] y = healthdisplay.GetComponentsInChildren<Slider>();
        y[0].value = value;
    }
    public void takedamage(int dam)
    {
        print("Agent take damage: " + dam);
        currenthealth -= dam;
        if (currenthealth < 0)
        {
            currenthealth = 0;
        }
    }
    public int getcurrenthealth()
    {
        return currenthealth;
    }
    private void OnCollisionEnter(Collision collision)
    {
        // to avoid continus collision
        if (Time.time > lastCollisionTime + collisionCooldown) {
            var collisionLayer = collision.collider.gameObject.layer;
            // if collision is not from missle
            if (collisionLayer != LayerMask.NameToLayer("Missle")) {
                takedamage(10);
            }
            lastCollisionTime = Time.time;
        }
    }
}
