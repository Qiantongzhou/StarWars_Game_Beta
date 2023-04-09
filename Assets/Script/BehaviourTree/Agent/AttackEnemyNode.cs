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
    private GameObject misslePrefab;

    public AttackEnemyNode(AgentBT btAgent, GameObject misslePrefab) {
        this.btAgent = btAgent;
        this.misslePrefab = misslePrefab;
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
        Debug.DrawLine(btAgent.missleLauncher.position, btAgent.missleLauncher.position + btAgent.missleLauncher.TransformDirection(Vector3.forward), Color.red, 0.1f);
        if (btAgent != null) {
            GameObject missle = AgentBT.Instantiate(btAgent.misslePrefab, btAgent.missleLauncher.position, btAgent.missleLauncher.rotation);
            missle.GetComponent<Missle>().target = btAgent.target;
        }
    }


}
