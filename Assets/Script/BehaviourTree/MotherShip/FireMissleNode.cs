using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FireMissleNode : ActionNode {
    private MSBT msbt;
    private float lastExecutedTime = 0f;
    private float executionInterval = 15f;
    private bool isFirstExecution = true;
    public FireMissleNode(MSBT msbt) {
        this.msbt = msbt;
    }

    public override NodeStatus Execute() {
        if (isFirstExecution) {
            isFirstExecution = false;
            FireMissle();
        }
        if (Time.time - lastExecutedTime > executionInterval) {
            FireMissle();
            lastExecutedTime = Time.time;
        }
        return NodeStatus.SUCCESS;
    }

    public void FireMissle() {
        if (msbt != null) {
            foreach (Transform child in msbt.missleLauncher) {
                GameObject missle = MSBT.Instantiate(msbt.misslePrefab, child.position, child.rotation);
                Missle missleComponent = missle.GetComponent<Missle>();
                missleComponent.target = msbt.target;
                missleComponent.missleSender = msbt.gameObject;
            }
        }
    }
}
