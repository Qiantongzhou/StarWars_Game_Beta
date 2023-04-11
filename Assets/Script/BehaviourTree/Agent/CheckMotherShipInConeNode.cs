using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Node;

public class CheckMotherShipInConeNode : ConditionNode {
    private AgentBT btAgent;
    private float dectectRadius;

    // initialize
    public CheckMotherShipInConeNode(AgentBT btAgent, float dectectRadius) {
        this.btAgent = btAgent;
        this.dectectRadius = dectectRadius;
    }
    public override NodeStatus Execute() {
        if (CheckForMotherShipInCone()) {
            return NodeStatus.SUCCESS;
        }
        else {
            return NodeStatus.FAILURE;
        }
    }

    // check if there is an enemy in the area
    private bool CheckForMotherShipInCone() {
        LayerMask mask = LayerMask.GetMask("Agent");
        Collider[] colliders = Physics.OverlapSphere(btAgent.transform.position, dectectRadius, mask);

        foreach (Collider col in colliders) {
            // check enemy mothership;
            if (col.name == "MotherShip" && !col.CompareTag(btAgent.tag)&& !CheckObstacleBetween(col.gameObject)) {
                return true;
            }
        }
        return false;
    }

    // Check if there is an obstacle between the agent and the target
    private bool CheckObstacleBetween(GameObject target) {
        Vector3 start = btAgent.transform.position;
        Vector3 end = target.transform.position - start;
        float maxDistance = end.magnitude + 2f;
        bool result = Physics.Raycast(start, end, out RaycastHit hit, maxDistance) && hit.collider.gameObject.layer != LayerMask.NameToLayer("Agent");
        return result;
    }
}
