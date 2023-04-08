using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TacticalManeuverNode : ActionNode
{
    private AgentBT btAgent;
    private float desired_heading = 90f;
    private float speed;
    private float turn_speed;
    private bool isManeuvering = false;
    private Coroutine tacticalManeuverCoroutine;

    public TacticalManeuverNode(AgentBT btAgent) {
        this.btAgent = btAgent;
        speed = btAgent.speed;
        turn_speed = btAgent.turnSpeed;
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
        Quaternion desiredRotation = btAgent.transform.rotation * Quaternion.Euler(RandomSign()*120f, RandomSign()*90f, 0f);
        float elapsedTime = 0f;
        float maneuverTime = 1f;
        while (elapsedTime < maneuverTime) {
            float t = elapsedTime / maneuverTime;
            btAgent.transform.rotation = Quaternion.Lerp(initialRotation, desiredRotation, t);
            btAgent.transform.Translate(Vector3.forward * Time.deltaTime * speed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        isManeuvering = false;
    }

    public void ClimbingTurn() {

    }
}
