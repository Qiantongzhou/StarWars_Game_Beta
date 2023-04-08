using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AstarMove2TargetNode : ActionNode {
    AStarAgent aStarAgent;
    AgentBT btAgent;
    Transform currentTarget;
    private float timer = 0f;
    private const float clearPathInterval = 0.4f;

    public AstarMove2TargetNode(AgentBT btAgent) {
        this.btAgent = btAgent;
        aStarAgent = btAgent.gameObject.GetComponent<AStarAgent>();
    }

    public override NodeStatus Execute() {
        MoveToTarget();
        return NodeStatus.SUCCESS;
    }

    private void MoveToTarget() {
        timer += Time.fixedDeltaTime;
        if (timer > clearPathInterval) {
            if(currentTarget.transform.position != btAgent.target.transform.position) {
                currentTarget = btAgent.target;
                aStarAgent.Pathfinding(currentTarget.position, false, false);
            }
            timer = 0f;
        }
        if (aStarAgent.Status == AStarAgentStatus.Finished) {
            currentTarget = btAgent.target;
            aStarAgent.Pathfinding(currentTarget.position, false, false);
        }
        if (aStarAgent.Status == AStarAgentStatus.Invalid) {
            currentTarget = btAgent.target;
            aStarAgent.Pathfinding(currentTarget.position, false, false);
        }
    }
}
