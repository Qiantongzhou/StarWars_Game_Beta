using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsTargetNode : ActionNode {
    Transform currentTargetTransform;
    AgentBT btAgent;
    float turnSpeed = 5f;
    Rigidbody rb;

    // initialize
    public MoveTowardsTargetNode(AgentBT btAgent) {
        this.btAgent = btAgent;
        this.rb = btAgent.GetComponent<Rigidbody>();
    }

    // execute
    public override NodeStatus Execute() {
        currentTargetTransform = btAgent.target;
        MoveTowardsTarget();
        return NodeStatus.SUCCESS;
    }

    public void MoveTowardsTarget() {
        if (currentTargetTransform != null) {
            Vector3 targetDirection = currentTargetTransform.position - btAgent.transform.position;
            if (targetDirection.magnitude > 1f) {
                Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
                rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, Time.deltaTime * turnSpeed));
                float distance = targetDirection.magnitude;
                if (distance > 30f) {
                    rb.MovePosition(rb.position + btAgent.transform.forward * btAgent.speed * Time.deltaTime);
                }
            }
        }
    }
}
