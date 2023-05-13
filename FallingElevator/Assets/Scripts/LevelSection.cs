using System;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelSection: MonoBehaviour
{
    public Transform endingPosition;

    private void Awake()
    {
        if (endingPosition == null)
        {
            // Find the go 
            endingPosition = transform.Find("endingPosition");
        }
    }
}
