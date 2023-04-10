using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Team
{
    Team1,
    Team2
}

public class TeamAssignment : MonoBehaviour
{
    public Team team;

    // Use this for initialization
    void Start()
    {
        if (team == Team.Team1)
        {
            gameObject.tag = "Team 1";
            Debug.Log("Assigned to team 1");
        }
        else if (team == Team.Team2)
        {
            // Do something for team 2 assignment
            gameObject.tag = "Team 2";
            Debug.Log("Assigned to team 2");
        }
    }
}
