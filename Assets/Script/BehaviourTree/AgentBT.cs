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
    public Transform missleLauncher;
    public GameObject misslePrefab;
    private AStarAgent aStarAgent;
    private AgentBT btAgent;
    private SelectorNode root;

    private void Start() {
        btAgent = GetComponent<AgentBT>();
        aStarAgent = GetComponent<AStarAgent>();
        aStarAgent.setSpeed(speed, turnSpeed);
        missleLauncher = transform.GetChild(1);

        // Build behavior tree
        root = new SelectorNode();

        SequenceNode emergencyEvasionSeq = new SequenceNode();
        emergencyEvasionSeq.AddChild(new CheckMissleInAreaNode(btAgent, 30f));
        emergencyEvasionSeq.AddChild(new TacticalManeuverNode(btAgent, 0.8f));

        SequenceNode proximityConflictEvasionSeq = new SequenceNode();
        proximityConflictEvasionSeq.AddChild(new CheckEnemyInProximityNode(btAgent, 80f));
        proximityConflictEvasionSeq.AddChild(new TacticalManeuverNode(btAgent, 1.1f));

        // if enemy in cone, attack;
        SequenceNode offensiveSeq = new SequenceNode();
        offensiveSeq.AddChild(new CheckEnemyInConeNode(btAgent, 500f, 3f));

        ParallelNode seekMovementParalle = new ParallelNode();
        seekMovementParalle.AddChild(new MoveTowardsTargetNode(btAgent));
        offensiveSeq.AddChild(new AttackEnemyNode(btAgent, misslePrefab));
        seekMovementParalle.AddChild(new ObstacleAvoidanceNode(btAgent, 100f, 30f, speed));
        offensiveSeq.AddChild(seekMovementParalle);

        

        // if enemy not in cone but target is set, move towards target
        SequenceNode traceSeq = new SequenceNode();
        traceSeq.AddChild(new AstarMove2TargetNode(btAgent));

        // if no enemy detected, check if mother ship was found
        //SequenceNode attackMotherShipSeq = new SequenceNode();
        //attackMotherShipSeq.AddChild(new AstarMove2TargetNode(btAgent));
        //attack 

        // else move Random
        SequenceNode wanderSeq = new SequenceNode();
        wanderSeq.AddChild(new AstarRandomMoveNode(btAgent));
        

        root.AddChild(emergencyEvasionSeq);
        root.AddChild(proximityConflictEvasionSeq);
        root.AddChild(offensiveSeq);
        root.AddChild(traceSeq);
        //root.AddChild(attackMotherShipSeq);
        root.AddChild(wanderSeq);
    }

    // Update is called once per frame
    void FixedUpdate() {
        root.Execute();
    }
}
