using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSBT : MonoBehaviour {
    public Transform target;
    public GameObject misslePrefab;
    private MSBT msbt;
    private SelectorNode root;
    public Transform[] missleLauncher;
   
    private void Start() {
        msbt = this;
        missleLauncher = new Transform[transform.GetChild(0).childCount];
        foreach (Transform child in transform.GetChild(0)) {
            missleLauncher[child.GetSiblingIndex()] = child;
            print(child);
        }



        // Build behavior tree
        root = new SelectorNode();
        SequenceNode testSeq = new SequenceNode();
        //testSeq.AddChild(new CheckTargetInAreaNode(msbt));
        testSeq.AddChild(new FireMissleNode(msbt));
        //testSeq.AddChild(new FireLaserCannonNode(msbt));

        root.AddChild(testSeq);
    }

    // Update is called once per frame
    void FixedUpdate() {
        root.Execute();
    }
}
