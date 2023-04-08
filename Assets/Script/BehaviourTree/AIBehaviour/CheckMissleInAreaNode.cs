using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMissleInAreaNode : ConditionNode {
    private AgentBT btAgent;
    private float dectectRadius;

    // initialize
    public CheckMissleInAreaNode(AgentBT btAgent, float dectectRadius) {
        this.btAgent = btAgent;
        this.dectectRadius = dectectRadius;
    }
    public override NodeStatus Execute() {
        if (CheckForMissleInArea()) {
            return NodeStatus.SUCCESS;
        }
        else {
            return NodeStatus.FAILURE;
        }
    }

    private bool CheckForMissleInArea() {
        LayerMask mask = LayerMask.GetMask("Missle");
        return Physics.CheckSphere(btAgent.transform.position, dectectRadius, mask);
    }
}
