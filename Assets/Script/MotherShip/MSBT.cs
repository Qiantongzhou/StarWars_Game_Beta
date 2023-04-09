using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSBT : MonoBehaviour {
    public bool debug;

    public GameObject misslePrefab;
    public float turnSpeed;
    public float defensiveRange;
    
    private MSBT msbt;
    private SelectorNode root;

    [HideInInspector] public Transform[] missleLauncher;
    [HideInInspector] public Transform laserNozzle;
    [HideInInspector] public Transform target;
   
    private void Start() {
        msbt = this;
        missleLauncher = new Transform[transform.GetChild(0).childCount];
        foreach (Transform child in transform.GetChild(0)) {
            missleLauncher[child.GetSiblingIndex()] = child;
        }
        laserNozzle = transform.GetChild(1);


        // Build behavior tree
        root = new SelectorNode();
        SequenceNode counterAttackSeq = new SequenceNode();
        counterAttackSeq.AddChild(new CheckTargetInAreaNode(msbt, defensiveRange));
        counterAttackSeq.AddChild(new FireMissleNode(msbt));
        counterAttackSeq.AddChild(new TurnTowardNode(msbt));
        counterAttackSeq.AddChild(new FireLaserCannonNode(msbt));

        root.AddChild(counterAttackSeq);
    }

    // Update is called once per frame
    void FixedUpdate() {
        root.Execute();
    }
}
