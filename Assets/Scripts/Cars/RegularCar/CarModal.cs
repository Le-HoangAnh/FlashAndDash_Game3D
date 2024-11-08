using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarModal : MonoBehaviour
{
    [SerializeField] private string carName;
    [SerializeField] private float carPrice;
    string label;
    int numberOfCheckpointsHit;
    float distanceToNextCheckpoint;

    public string GetCarName()
    {
        return carName;
    }

    public string GetCarLabel()
    {
        return label;
    }

    public void SetCarLabel(string label)
    {
        this.label = label;
    }

    public int GetNumberOfCheckpointsHit()
    {
        return numberOfCheckpointsHit;
    }

    public float GetCarPrice()
    {
        return carPrice;
    }

    public void CheckpointWasHit()
    {
        numberOfCheckpointsHit++;
    }

    public float GetDistanceToNextCheckpoint()
    {
        return distanceToNextCheckpoint;
    }

    public void SetDistanceToNextCheckpoint(float distance)
    {
        distanceToNextCheckpoint = distance;
    }
}
