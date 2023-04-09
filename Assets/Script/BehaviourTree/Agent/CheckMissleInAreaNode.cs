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
        Collider missle = FindClosestColliderInSphere();
        if (missle == null) {
            return false;
        }
        if (missle.GetComponent<Missle>().target.CompareTag(btAgent.tag)) {
            return true;
        }
        else {
            return false;
        }
    }
    private Collider FindClosestColliderInSphere() {
        LayerMask mask = LayerMask.GetMask("Missle");
        Collider[] colliders = Physics.OverlapSphere(btAgent.transform.position, dectectRadius, mask);

        Collider closestCollider = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider col in colliders) {
            float distance = Vector3.Distance(col.transform.position, btAgent.transform.position);
            if (distance < closestDistance) {
                closestCollider = col;
                closestDistance = distance;
            }
        }

        return closestCollider;
    }

}
