using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Node;

public class CheckTargetInAreaNode : ConditionNode {
    private MSBT msbt;
    private float dectectRadius;


    // initialize
    public CheckTargetInAreaNode(MSBT msbt, float dectectRadius) {
        this.msbt = msbt;
        this.dectectRadius = dectectRadius;
    }
    public override NodeStatus Execute() {
        if (CheckForEnemiesArea()) {
            return NodeStatus.SUCCESS;
        }
        else {
            return NodeStatus.FAILURE;
        }
    }

    // check if there is an enemy in the area
    private bool CheckForEnemiesArea() {
        LayerMask mask = LayerMask.GetMask("Agent");
        Collider[] colliders = Physics.OverlapSphere(msbt.transform.position, dectectRadius, mask);

        Collider closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider col in colliders) {
            if (!CheckObstacleBetween(col.gameObject)) {
                float distance = Vector3.Distance(msbt.transform.position, col.transform.position);
                if (distance < closestDistance) {
                    closestEnemy = col;
                    closestDistance = distance;
                }
            }
        }

        if (closestEnemy != null) {
            if (closestEnemy.gameObject.CompareTag("Player")) {
                msbt.target = closestEnemy.gameObject.transform.parent;
            }
            else {
                msbt.target = closestEnemy.gameObject.transform.parent.parent;
            }
            if (msbt.debug) {
                Debug.DrawRay(closestEnemy.gameObject.transform.position, Vector3.up, Color.red, 5f);
            }
            return true;
        }

        return false;
    }

    // Check if there is an obstacle between the agent and the target
    private bool CheckObstacleBetween(GameObject target) {
        Vector3 start = msbt.transform.position;
        Vector3 end = target.transform.position - start;
        float maxDistance = end.magnitude + 2f;
        bool result = Physics.Raycast(start, end, out RaycastHit hit, maxDistance) && hit.collider.gameObject != target;
        return result;
    }

}
