using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TacticalManeuverNode : ActionNode
{
    private float maneuverTime;
    private AgentBT btAgent;
    private float desired_heading = 90f;
    private float speed;
    private float turn_speed;
    private bool isManeuvering = false;
    private Coroutine tacticalManeuverCoroutine;
    private Rigidbody rb;

    public TacticalManeuverNode(AgentBT btAgent, float maneuverTime) {
        this.btAgent = btAgent;
        speed = btAgent.speed;
        turn_speed = btAgent.turnSpeed;
        this.maneuverTime = maneuverTime;
        this.rb = btAgent.GetComponent<Rigidbody>();
    }

    public override NodeStatus Execute() {
        if (!isManeuvering) {
            if (tacticalManeuverCoroutine != null) {
                btAgent.StopCoroutine(tacticalManeuverCoroutine);
            }
            isManeuvering = true;
            tacticalManeuverCoroutine = btAgent.StartCoroutine(TacticalManeuver());
        }
        return NodeStatus.SUCCESS;
    }

    public int RandomSign() {
        int sign = Random.Range(0, 2) * 2 - 1;
        return sign;
    }

    public IEnumerator TacticalManeuver() {
        Vector3 initialPosition = btAgent.transform.position;
        Quaternion initialRotation = btAgent.transform.rotation;
        Quaternion desiredRotation = btAgent.transform.rotation * Quaternion.Euler(RandomSign()*120f, 90f, 0f);
        float elapsedTime = 0f;
        maneuverTime *= Random.Range(0.9f, 1.3f);
        while (elapsedTime < maneuverTime) {
            float t = elapsedTime / maneuverTime;
            btAgent.transform.rotation = Quaternion.Lerp(initialRotation, desiredRotation, t);
            rb.MovePosition(rb.position + btAgent.transform.forward * speed * Time.deltaTime);
            //btAgent.transform.Translate(Vector3.forward * Time.deltaTime * speed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        isManeuvering = false;
    }

    public void ClimbingTurn() {

    }
}
