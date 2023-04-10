using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Team
{
    Team1,
    Team2
}


public class TeamAssignments : MonoBehaviour
{
    public Team team;

    // Use this for initialization
    void Start()
    {
        if (team == Team.Team1)
        {
            // Do something for team 1 assignment
            Debug.Log("Assigned to team 1");
        }
        else if (team == Team.Team2)
        {
            // Do something for team 2 assignment
            Debug.Log("Assigned to team 2");
        }
    }
}
