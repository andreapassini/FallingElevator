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

    public float minVerticalSpeed = 5f;
    public float maxVerticalSpeed = 25f;
    public float acceleration = .5f;

    private float _verticalSpeed = 0f;

    private void Start()
    {
        _verticalSpeed = minVerticalSpeed;
    }

    public void Hit(Transform transformHitter)
    {
        Debug.Log("Hit Obstacle");
        
        onHitObstacle?.Invoke();
    }

    // private void FixedUpdate()
    // {
    //     _verticalSpeed += (_verticalSpeed * acceleration) * Time.fixedDeltaTime;
    //
    //     if (_verticalSpeed >= maxVerticalSpeed)
    //         _verticalSpeed = maxVerticalSpeed;
    //
    //     transform.position = new Vector3(transform.position.x, transform.position.y + _verticalSpeed);
    // }
}
