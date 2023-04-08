using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Node;
using static UnityEngine.GraphicsBuffer;

public class TurnTowardNode : ActionNode {
    private MSBT msbt;
    private LayerMask targetLayer;

    public TurnTowardNode(MSBT msbt) {
        this.msbt = msbt;
    }
    public override NodeStatus Execute() {
        Vector3 directionToTarget = msbt.target.position - msbt.laserNozzle.transform.position;
        Vector3 forward = msbt.laserNozzle.transform.forward;
        float distance = directionToTarget.magnitude;

        float angle = Vector3.Angle(directionToTarget, forward);

        if (angle <= 120f && distance > 30f) {
            TurnToward();
            Ray ray = new Ray(msbt.laserNozzle.transform.position, msbt.laserNozzle.transform.forward);
            if (Physics.Raycast(ray, out _, Mathf.Infinity, 1 << msbt.target.gameObject.layer)) {
                return NodeStatus.SUCCESS;
            }
        }
        return NodeStatus.RUNNING;
    }

    public void TurnToward() {
        Vector3 targetDirection = msbt.target.transform.position - msbt.laserNozzle.transform.position;
        if (targetDirection.magnitude > 1f) {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
            msbt.gameObject.transform.rotation = Quaternion.Slerp(msbt.gameObject.transform.rotation, targetRotation, Time.deltaTime * msbt.turnSpeed);
        }
    }

}
