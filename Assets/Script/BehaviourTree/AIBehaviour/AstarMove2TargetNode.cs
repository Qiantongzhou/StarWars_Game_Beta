using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstarMove2TargetNode : ActionNode {
    AStarAgent aStarAgent;
    AgentBT btAgent;
    Transform currentTarget;

    public AstarMove2TargetNode(AgentBT btAgent) {
        this.btAgent = btAgent;
        aStarAgent = btAgent.gameObject.GetComponent<AStarAgent>();

        currentTarget = btAgent.target;
        aStarAgent.Pathfinding(currentTarget.position);
    }

    public override NodeStatus Execute() {
        MoveToTarget();
        return NodeStatus.SUCCESS;
    }

    private void MoveToTarget() {
        if (aStarAgent.Status == AStarAgentStatus.Invalid) {
            currentTarget = btAgent.target;
            aStarAgent.Pathfinding(currentTarget.position);
        }
    }
}
