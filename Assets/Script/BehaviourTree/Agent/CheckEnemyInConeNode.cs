using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;
using static Node;

public class CheckEnemyInConeNode : ConditionNode {
    private AgentBT btAgent;
    private float dectectRadius;
    private float timer;
    private float clearTargetInterval;

    // initialize
    public CheckEnemyInConeNode(AgentBT btAgent, float dectectRadius, float clearTargetInterval) {
        this.btAgent = btAgent;
        this.dectectRadius = dectectRadius;
        this.clearTargetInterval = clearTargetInterval;
    }
    public override NodeStatus Execute() {
        if (CheckForEnemiesInCone()) {
            timer = 0f;
            return NodeStatus.SUCCESS;
        }
        else {
            timer += Time.deltaTime;
            if (timer >= clearTargetInterval) {
                btAgent.target = null;
                timer = 0f;
            }
            return NodeStatus.FAILURE;
        }
    }

    // check if there is an enemy in the area
    private bool CheckForEnemiesInCone() {
        LayerMask mask = LayerMask.GetMask("Agent");
        Collider[] colliders = Physics.OverlapSphere(btAgent.transform.position, dectectRadius, mask);

        Collider closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider col in colliders) {
            // check enemy mothership;
            if(col.name == "MotherShip" && !col.CompareTag(btAgent.tag)) {
                btAgent.motherShipTarget = col.gameObject.transform;
                continue;
            }
            // if ally, continue;
            if (col.CompareTag(btAgent.tag)) {
                continue;
            }
            if (!col.CompareTag("Team1") && !col.CompareTag("Team2")) {
                continue;
            }
            // then col is enemy agent;
            
            Vector3 directionToTarget = col.transform.position - btAgent.transform.position;
            Vector3 forward = btAgent.transform.forward;

            float distance = directionToTarget.magnitude;

            // this angle is to check if the target is in the cone
            float angle = Vector3.Angle(directionToTarget, forward);

            // check in Cone
            if (angle <= 180f) {
                // check obstacle
                if (!CheckObstacleBetween(col.gameObject)) {
                    if (distance < closestDistance) {
                        closestEnemy = col;
                        closestDistance = distance;
                    }
                }
            }
        }
        
        if (closestEnemy != null) {
            btAgent.target = closestEnemy.gameObject.transform;
            return true;
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
