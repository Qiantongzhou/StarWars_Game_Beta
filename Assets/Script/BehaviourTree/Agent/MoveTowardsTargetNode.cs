using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsTargetNode : ActionNode {
    Transform currentTargetTransform;
    AgentBT btAgent;
    float turnSpeed = 5f;
    Rigidbody rb;
    bool isForMotherShip = false;

    // initialize
    public MoveTowardsTargetNode(AgentBT btAgent, bool isForMotherShip = false) {
        this.btAgent = btAgent;
        this.rb = btAgent.GetComponent<Rigidbody>();
        this.isForMotherShip = isForMotherShip;
    }

    // execute
    public override NodeStatus Execute() {
        if(!isForMotherShip) {
            currentTargetTransform = btAgent.target;
        }
        else {
            currentTargetTransform = btAgent.motherShipTarget;
        }
       
        MoveTowardsTarget();
        return NodeStatus.SUCCESS;
    }

    public void MoveTowardsTarget() {
        float stopDistance = 30f;
        if(isForMotherShip) {
            stopDistance = 60f;
        }

        if (currentTargetTransform != null) {
            Vector3 targetDirection = currentTargetTransform.position - btAgent.transform.position;
            if (targetDirection.magnitude > 1f) {
                Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
                rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, Time.deltaTime * turnSpeed));
                float distance = targetDirection.magnitude;
                if (distance > stopDistance) {
                    rb.MovePosition(rb.position + btAgent.transform.forward * btAgent.speed * Time.deltaTime);
                }
            }
        }
    }
}
