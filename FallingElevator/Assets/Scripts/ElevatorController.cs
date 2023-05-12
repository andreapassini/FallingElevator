using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class ElevatorController : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private Collider2D _collider2D;

    private bool _moveRight = false;
    private Vector2 _vecForce;
    
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
            _moveRight = true;
        }
        else
        {
            _moveRight = false;
        }
    }

    private void FixedUpdate()
    {
        if (_moveRight)
        {
            _vecForce = Vector2.right;
        }
        else
        {
            _vecForce = Vector2.left;
        }
        
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
}
