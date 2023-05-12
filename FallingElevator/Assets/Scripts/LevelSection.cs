using System;
using UnityEngine;

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
