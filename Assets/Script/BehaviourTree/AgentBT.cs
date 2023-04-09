using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AgentBT : MonoBehaviour {
    public float speed = 1f;
    public float turnSpeed = 1f;
    public Transform target;
    public Transform motherShipTarget;
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
        // if enemy in cone, attack;
        SequenceNode offensiveSeq = new SequenceNode();
        offensiveSeq.AddChild(new CheckEnemyInConeNode(btAgent, 500f, 10f));

        ParallelNode seekMovementParalle = new ParallelNode();
        seekMovementParalle.AddChild(new MoveTowardsTargetNode(btAgent));
        seekMovementParalle.AddChild(new ObstacleAvoidanceNode(btAgent, 100f, 30f, speed));
        offensiveSeq.AddChild(seekMovementParalle);

        offensiveSeq.AddChild(new AttackEnemyNode(btAgent));

        // if enemy not in cone but target is set, move towards target
        SequenceNode traceSeq = new SequenceNode();
        traceSeq.AddChild(new AstarMove2TargetNode(btAgent));

        // if no enemy detected, check if mother ship was found

        // else move Random
        SequenceNode testSeq = new SequenceNode();
        testSeq.AddChild(new AstarRandomMoveNode(btAgent));
        

        root.AddChild(emergencyEvasionSeq);
        root.AddChild(offensiveSeq);
        root.AddChild(traceSeq);
        //root.AddChild(testSeq);
    }

    // Update is called once per frame
    void FixedUpdate() {
        root.Execute();
    }
}
