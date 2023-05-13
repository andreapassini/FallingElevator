using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle: MonoBehaviour, IHittable
{
    #region Event 
    public delegate void OnHitObstacle();
    public static event OnHitObstacle onHitObstacle;
    #endregion

    public void Hit(Transform transformHitter)
    {
        Debug.Log("Hit Obstacle");
        
        onHitObstacle?.Invoke();
    }
}
