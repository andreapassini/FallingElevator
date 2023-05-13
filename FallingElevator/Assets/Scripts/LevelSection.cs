using System;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelSection: MonoBehaviour
{
    public Transform endingPosition;
    [HideInInspector]
    public float spawnTime;
    private void Awake()
    {
        if (endingPosition == null)
        {
            // Find the go 
            endingPosition = transform.Find("endingPosition");
        }
    }
}
