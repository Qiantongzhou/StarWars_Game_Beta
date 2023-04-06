using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentBT : MonoBehaviour {
    public Transform target;
    private AgentBT btAgent;
    private SelectorNode root;

    private void Start() {
        btAgent = GetComponent<AgentBT>();

        // Build behavior tree
        root = new SelectorNode();
        SequenceNode testSeq = new SequenceNode();
        //testSeq.AddChild(new AstarRandomMoveNode(btAgent));
        testSeq.AddChild(new AstarMove2TargetNode(btAgent));
        testSeq.AddChild(new TurnTowardsTargetNode(btAgent));

        root.AddChild(testSeq);
    }

    // Update is called once per frame
    void FixedUpdate() {
        root.Execute();
    }
}
