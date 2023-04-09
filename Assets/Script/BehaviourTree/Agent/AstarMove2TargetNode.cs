using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting.FullSerializer;
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
        currentTarget = btAgent.target;
    }

    public override NodeStatus Execute() {
        if (btAgent.target != null) {
            MoveToTarget();
            return NodeStatus.SUCCESS;
        }
        else {
            return NodeStatus.FAILURE;
        }
    }

    private void MoveToTarget() {
        if (btAgent.target != null) {
            timer += Time.fixedDeltaTime;
            if (timer > clearPathInterval) {
                if (currentTarget == null || currentTarget.transform.position != btAgent.target.transform.position) {
                    currentTarget = btAgent.target;
                    aStarAgent.Pathfinding(currentTarget.position, false);
                }
                timer = 0f;
            }
            if (aStarAgent.Status == AStarAgentStatus.Finished) {
                currentTarget = btAgent.target;
                aStarAgent.Pathfinding(currentTarget.position, false);
            }
            if (aStarAgent.Status == AStarAgentStatus.Invalid) {
                currentTarget = btAgent.target;
                aStarAgent.Pathfinding(currentTarget.position, false);
            }
        }
    }
}
