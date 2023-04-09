using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnemyNode : ActionNode 
{
    private AgentBT btAgent;
    private float lastExecutedTime = 0f;
    private float executionInterval = 0.1f;
    private bool isFirstExecution = true;

    public AttackEnemyNode(AgentBT btAgent) {
        this.btAgent = btAgent;
    }

    public override NodeStatus Execute() {
        if (isFirstExecution) {
            isFirstExecution = false;
            Fire();
        }
        if (Time.time - lastExecutedTime > executionInterval) {
            Fire();
            lastExecutedTime = Time.time;
        }
        return NodeStatus.SUCCESS;
    }

    public void Fire() {
        if (btAgent != null) {
            Debug.Log("Fire");
        }
    }


}
