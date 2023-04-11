using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CheckEnemyInProximityNode : ConditionNode {
    private AgentBT btAgent;
    private float evadeDistance;


    public CheckEnemyInProximityNode(AgentBT btAgent, float evadeDistance) {
        this.btAgent = btAgent;
        this.evadeDistance = evadeDistance;
    }

    public override NodeStatus Execute() {
        if(btAgent.target != null) {
            float distance = Vector3.Distance(btAgent.transform.position, btAgent.target.position);
            float angle = Vector3.Angle(btAgent.transform.forward, btAgent.target.transform.forward);
            if (angle > 160f && distance < evadeDistance) {
                return NodeStatus.SUCCESS;
            }else if(distance < 30f) {
                return NodeStatus.SUCCESS;
            }
            else {
                return NodeStatus.FAILURE;
            }
        }
        return NodeStatus.FAILURE;
    }

}
