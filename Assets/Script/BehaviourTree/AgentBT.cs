using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AgentBT : MonoBehaviour {
    public float speed = 1f;
    public float turnSpeed = 1f;
    [HideInInspector] public Transform target;
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
        //SequenceNode offensiveSeq = new SequenceNode();
        //offensiveSeq.AddChild(new CheckEnemyInConeNode(btAgent, 30f));
        //offensiveSeq.AddChild(new AstarMove2TargetNode(btAgent));
        //offensiveSeq.AddChild(new TurnTowardsTargetNode(btAgent));
        //offensiveSeq.AddChild(new AttackEnemyNode(btAgent));
        SequenceNode testSeq = new SequenceNode();
        testSeq.AddChild(new AstarRandomMoveNode(btAgent));
        

        root.AddChild(emergencyEvasionSeq);
        root.AddChild(testSeq);
    }

    // Update is called once per frame
    void FixedUpdate() {
        root.Execute();
    }
}
