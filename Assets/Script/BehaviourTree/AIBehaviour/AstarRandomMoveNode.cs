using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AstarRandomMoveNode : ActionNode {
    AStarAgent aStarAgent;
    List<Point> freePoints;
    Point targetPoint;

    // initialize
    public AstarRandomMoveNode(AgentBT btAgent) {
        aStarAgent = btAgent.gameObject.GetComponent<AStarAgent>();

        SetRandomTarget();
        aStarAgent.Pathfinding(targetPoint.WorldPosition);
    }

    // execute
    public override NodeStatus Execute() {
        MoveToRandomTarget();
        return NodeStatus.SUCCESS;
    }

    public void MoveToRandomTarget() {
        if (aStarAgent.Status == AStarAgentStatus.Finished) {
            SetRandomTarget();
            aStarAgent.Pathfinding(targetPoint.WorldPosition);
        }
    }

    private void SetRandomTarget() {
        freePoints = WorldManager.Instance.GetFreePoints();
        targetPoint = freePoints[Random.Range(0, freePoints.Count)];
    }
}
