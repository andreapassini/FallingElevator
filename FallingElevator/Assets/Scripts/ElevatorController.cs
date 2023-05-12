using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class ElevatorController : MonoBehaviour
{
    public float minForce = 10f;
    public float maxForce = 25f;
    
    private Rigidbody2D _rigidbody2D;
    private Collider2D _collider2D;

    private bool _moveRight = false;
    private Vector2 _vecForce;
    private bool _directionChanged = false;

    private float _increment;
    public float step = .5f;
    
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<Collider2D>();
    }

    private void Update()
    {
        // Catch the input
        if (Input.GetKey(KeyCode.Space))
        {
            if (!_moveRight)
            {
                _directionChanged = true;
                Debug.Log("Left to Right");
            }
            else
            {
                _directionChanged = false;
            }
            _moveRight = true;
            _vecForce = Vector2.right * InterpolateForce(_directionChanged);
        }
        else
        {
            if (_moveRight)
            {
                _directionChanged = true;
                Debug.Log("Right to Left");
            }
            else
            {
                _directionChanged = false;
            }
            
            _moveRight = false;
            _vecForce = Vector2.left * InterpolateForce(_directionChanged);
        }
    }

    private void FixedUpdate()
    {
        Debug.Log(_vecForce.x);
        // Add the forces to the rb
        _rigidbody2D.AddForce(_vecForce);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.TryGetComponent<IHittable>(out IHittable hit))
        {
            hit.Hit(transform);
        }
    }

    private float InterpolateForce(bool reset)
    {
        if (reset)
        {
            _increment = 0f;
            return minForce;
        }

        _increment += step * Time.fixedDeltaTime;
        return Mathf.Lerp(minForce, maxForce, _increment);
    }
}
