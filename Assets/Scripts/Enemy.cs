using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    
    public RuntimeAnimatorController[] animators;
    public Rigidbody2D target;

    private bool _isAlive;
    Rigidbody2D _rigidbody;
    SpriteRenderer _spriteRenderer;
    Animator _animator;
    
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
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

    private void OnEnable()
    {
        _isAlive = true;
        target = GameManager.Instance.player.GetComponent<Rigidbody2D>();
        health = maxHealth;
        
    }

    public void Init(SpawnData data)
    {
        _animator.runtimeAnimatorController = animators[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Bullet")) return;

        health -= other.GetComponent<Bullet>().damage;

        if (health > 0)
        {
            // hit
        }
        else
        {
            // dead
            Dead();
        }
    }

    void Dead()
    {
        gameObject.SetActive(false);
    }
}
