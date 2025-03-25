using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 inputVector;
    public float speed;
    public Scanner scanner;

    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private static readonly int Speed = Animator.StringToHash("speed");

    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
    }

    void FixedUpdate()
    {
        Vector2 nextVector = inputVector * (speed * Time.fixedDeltaTime);
        //_rigidbody2D.AddForce(inputVector); //힘을 가하기
        //_rigidbody2D.velocity = inputVector; //속도를 바꾸기
        _rigidbody2D.MovePosition(_rigidbody2D.position + nextVector); //위치를 바꾸기
        
    }

    private void LateUpdate()
    {
        _animator.SetFloat(Speed,inputVector.magnitude);
        
        if (inputVector.x != 0)
        {
            _spriteRenderer.flipX = inputVector.x < 0;
        }
    }

    void OnMove(InputValue value)
    {
        inputVector = value.Get<Vector2>();
    }
}
