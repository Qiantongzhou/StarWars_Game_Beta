using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAvoidanceNode : ActionNode {
    private float MaxAvoidDistance = 2.0f;   
    private float AvoidAngle = 60.0f;        
    private float ForceMultiplier = 1.0f;    
    private AgentBT btAgent;


    public ObstacleAvoidanceNode(AgentBT btAgent, float MaxAvoidDistance, float AvoidAngle, float ForceMultiplier) {
        this.btAgent = btAgent;
        this.MaxAvoidDistance = MaxAvoidDistance;
        this.AvoidAngle = AvoidAngle;
        this.ForceMultiplier = ForceMultiplier;
    }
    public override NodeStatus Execute() {
        RaycastHit hit;
        if (Physics.Raycast(btAgent.transform.position, btAgent.transform.forward, out hit, MaxAvoidDistance)) {
            Vector3 avoidDirection = Vector3.zero;
            float angle = Vector3.Angle(btAgent.transform.forward, hit.normal);
            if (angle < AvoidAngle) {
                avoidDirection = Vector3.Cross(btAgent.transform.up, hit.normal);
            }
            else {
                avoidDirection = Vector3.Cross(btAgent.transform.up, Vector3.Cross(btAgent.transform.forward, hit.normal));
            }

            Rigidbody rigidbody = btAgent.GetComponent<Rigidbody>();
            if (rigidbody != null) {
                rigidbody.AddForce(avoidDirection.normalized * ForceMultiplier);
            }

            return NodeStatus.SUCCESS;
        }

        return NodeStatus.FAILURE;
    }
}
