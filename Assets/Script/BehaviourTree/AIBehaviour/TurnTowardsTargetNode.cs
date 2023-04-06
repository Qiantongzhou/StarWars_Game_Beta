using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTowardsTargetNode : ActionNode {
    Transform currentTargetTransform;
    AgentBT btAgent;
    float turnSpeed = 5f;

    // initialize
    public TurnTowardsTargetNode(AgentBT btAgent) {
        this.btAgent = btAgent;
        currentTargetTransform = btAgent.target;
    }

    // execute
    public override NodeStatus Execute() {
        TurnTowardsTarget();
        return NodeStatus.SUCCESS;
    }

    public void TurnTowardsTarget() {
        if (currentTargetTransform != null) {
            Vector3 targetDirection = currentTargetTransform.position - btAgent.transform.position;
            if (targetDirection.magnitude > 1f) {
                Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
                btAgent.gameObject.transform.rotation = Quaternion.Slerp(btAgent.gameObject.transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
            }
        }
    }
}
