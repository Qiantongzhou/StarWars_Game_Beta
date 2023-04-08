using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentBT : MonoBehaviour {
    public Transform target;
    public float speed = 1f;
    public float turnSpeed = 1f;
    private AStarAgent aStarAgent;
    private AgentBT btAgent;
    private SelectorNode root;

    private void Start() {
        btAgent = GetComponent<AgentBT>();
        aStarAgent = GetComponent<AStarAgent>();
        aStarAgent.setSpeed(speed, turnSpeed);


        // Build behavior tree
        root = new SelectorNode();
        
        SequenceNode emergencyEvasionSeq = new SequenceNode();
        emergencyEvasionSeq.AddChild(new CheckMissleInAreaNode(btAgent, 30f));
        emergencyEvasionSeq.AddChild(new TacticalManeuverNode(btAgent));
        SequenceNode testSeq = new SequenceNode();
        testSeq.AddChild(new AstarRandomMoveNode(btAgent));
        //testSeq.AddChild(new AstarMove2TargetNode(btAgent));
        //testSeq.AddChild(new TurnTowardsTargetNode(btAgent));

        root.AddChild(emergencyEvasionSeq);
        root.AddChild(testSeq);
    }

    // Update is called once per frame
    void FixedUpdate() {
        root.Execute();
    }
}
