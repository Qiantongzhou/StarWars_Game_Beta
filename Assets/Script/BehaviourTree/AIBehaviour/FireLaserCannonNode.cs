using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Node;
using static UnityEngine.GraphicsBuffer;

public class FireLaserCannonNode : ActionNode {
    private MSBT msbt;
    private float lastExecutedTime = 0f;
    private float executionInterval = 5f;
    private ProgressControlV3D laser;
    private bool isFirstExecution = true;
    public FireLaserCannonNode(MSBT msbt) {
        this.msbt = msbt;
        laser = msbt.laserNozzle.transform.GetChild(0).GetComponent<ProgressControlV3D>();
    }

    public override NodeStatus Execute() {
        if (isFirstExecution) { 
            isFirstExecution = false;
            msbt.StartCoroutine(FireLaserCannon(2f));
        }
        if (Time.time - lastExecutedTime > executionInterval) {
            msbt.StartCoroutine(FireLaserCannon(2f));
        }
        return NodeStatus.SUCCESS;
    }

    private IEnumerator FireLaserCannon(float seconds) {
        float elapsed = 0f;
        while (elapsed < seconds) {
            laser.StartLaserFire();
            elapsed += Time.deltaTime;
            lastExecutedTime = Time.time;
            yield return null;
        }
    }
}
