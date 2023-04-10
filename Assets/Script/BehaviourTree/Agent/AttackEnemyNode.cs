using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackEnemyNode : ActionNode 
{
    private AgentBT btAgent;
    private float lastExecutedTime = 0f;
    private float executionInterval = 0.1f;
    private bool isFirstExecution = true;

    public AttackEnemyNode(AgentBT btAgent, GameObject misslePrefab) {
        this.btAgent = btAgent;
    }

    public override NodeStatus Execute() {
        Vector3 directionToTarget = btAgent.target.position - btAgent.transform.position;
        float angle = Vector3.Angle(btAgent.transform.forward, directionToTarget);
        if (angle < 30f) {
            if (isFirstExecution) {
                isFirstExecution = false;
                Fire();
            }
            if (Time.time - lastExecutedTime > executionInterval) {
                Fire();
                lastExecutedTime = Time.time;
            }
        }
        return NodeStatus.SUCCESS;
    }

    public void Fire() {
        if (btAgent != null) {
            GameObject missle = AgentBT.Instantiate(btAgent.misslePrefab, btAgent.missleLauncher.position, btAgent.missleLauncher.rotation);
            missle.GetComponent<Missle>().target = btAgent.target;
        }
    }


}
