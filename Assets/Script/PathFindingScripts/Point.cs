using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// this class is to store the data of the point
/// </summary>
[System.Serializable]
public class Point
{
    public Vector3Int Coords;
    public Vector3 WorldPosition;
    public List<Vector3Int> Neighbours;
    public bool Invalid;
    public List<MovingData> MovingData;
    public float distanceFactor = 0.5f;
    public Point()
    {
        Neighbours = new List<Vector3Int>();
    }

    // Use this for Point initialization
    // 1) if MovingData is null, then create a new one
    // 2) sorting through the MovingData list, if null then add data
    // 3) if not null, then update the data
    public void AddMovingData(AStarAgent obj, float time,bool stationary=false)
    {
        if (MovingData == null)
        {
            MovingData = new List<MovingData>();
        }
        MovingData existing = MovingData.Find(x => x.MovingObj == obj);
        if (existing == null)
        {
            MovingData.Add(new MovingData() { MovingObj = obj, TimeToReach = time, TimeStarted = Time.time,Stationary=stationary });
        }
        else
        {
            existing.TimeStarted = Time.time;
            existing.TimeToReach = time;
            existing.Stationary = stationary;
        }
    }

    // Use this to remove data from MovingData list
    public void RemoveMovingData(CharacterMoveControl obj)
    {
        MovingData.Remove(MovingData.Find(x => x.MovingObj == obj));
    }

    // this method is to check if there is any intersection between the moving objects
    // if so remove some moving data to ensure no intersection
    public void CheckForIntersections()
    {
        if (MovingData != null)
        {
            List<MovingData> toRemove = new List<MovingData>();
            // Comparing every moving data in moveing data list
            for (int i = 0; i < MovingData.Count; i++)
            {
                MovingData data = MovingData[i];
                for (int j = 0; j < MovingData.Count; j++)
                {
                    if (i != j)
                    {
                        MovingData data2 = MovingData[j];
                        if (data2.Stationary)
                        {
                            toRemove.Add(data);
                            break;
                        }
                        // if the priority level is different
                        if (data.MovingObj.Priority < data2.MovingObj.Priority )
                        {
                            float ttReach = data.TrueTimeToReach();
                            float ttReach2 = data2.TrueTimeToReach();
                            if (ttReach <= 0 || ttReach2 <= 0)
                            {
                                continue;
                            }
                            float difference = Mathf.Abs(data.TrueTimeToReach() - data2.TrueTimeToReach());
                            // if the time to reach is different, remove the data
                            if (difference < distanceFactor)
                            {
                                toRemove.Add(data);
                                break;
                            }
                        }
                    }
                }
            }
            // remove the data
            for (int i = 0; i < toRemove.Count; i++)
            {
                MovingData.Remove(toRemove[i]);
            }
            // re-path the moving objects
            for (int i = 0; i < toRemove.Count; i++)
            {
                toRemove[i].MovingObj.RePath();
            }
        }
    }

    // check if the point is available 
    // if moving data is not null, it will walk through the data
    // if the data is stationary, then return false
    // if there is any data with higher priority, then check the time to reach
    public bool CheckPointAvailability(float timeToReach, int priority)
    {
        bool available = true;
        if (MovingData != null)
        {
            List<MovingData> toRemove = new List<MovingData>();
            for (int i = 0; i < MovingData.Count; i++)
            {
                if (MovingData[i].Stationary)
                {
                    return false;
                }
                if (MovingData[i].MovingObj.Priority > priority)
                {
                    float ttReach = MovingData[i].TrueTimeToReach();
                    if (ttReach <= 0)
                    {
                        toRemove.Add(MovingData[i]);
                        continue;
                    }
                    float difference = Mathf.Abs(ttReach - timeToReach);
                    if (difference < distanceFactor)
                    {
                        available = false;
                        break;
                    }
                }
            }
            for (int i = 0; i < toRemove.Count; i++)
            {
                MovingData.Remove(toRemove[i]);
            }
        }
        return available;
    }
}