using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public Rigidbody2D target;

    private bool _isAlive = true;
    Rigidbody2D _rigidbody;
    SpriteRenderer _spriteRenderer;
    
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (!_isAlive) return;
        Vector2 directionVector = target.position - _rigidbody.position;
        Vector2 nextVector = directionVector.normalized * (speed * Time.fixedDeltaTime);
        _rigidbody.MovePosition(_rigidbody.position + nextVector);
        _rigidbody.velocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        if (!_isAlive) return;
        _spriteRenderer.flipX = target.position.x < _rigidbody.position.x;
    }
}
