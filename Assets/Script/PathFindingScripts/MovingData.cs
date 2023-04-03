using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this class is to store the data of the moving object
/// </summary>
public class MovingData {
    public AStarAgent MovingObj;
    public float TimeToReach;
    public float TimeStarted;
    public bool Stationary;

    public float TrueTimeToReach() {
        if (TimeToReach == 0) {
            return 0;
        } 
        return Mathf.Max(TimeToReach - (Time.time - TimeStarted), 0);
    }
}
