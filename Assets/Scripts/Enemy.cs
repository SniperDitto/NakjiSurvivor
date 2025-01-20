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
    private Collider2D _collider;
    SpriteRenderer _spriteRenderer;
    Animator _animator;
    private WaitForFixedUpdate _waitForFixedUpdate;
    
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider =  GetComponent<Collider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _waitForFixedUpdate = new WaitForFixedUpdate();
    }

    void FixedUpdate()
    {
        if (!_isAlive || _animator.GetCurrentAnimatorStateInfo(0).IsName("Hit") ) return;
        Vector2 directionVector = target.position - _rigidbody.position;
        Vector2 nextVector = directionVector.normalized * (speed * Time.fixedDeltaTime);
        _rigidbody.MovePosition(_rigidbody.position + nextVector);
        _rigidbody.linearVelocity = Vector2.zero;
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
        _isAlive = true;
        _collider.enabled = true;
        _rigidbody.simulated = true;
        _spriteRenderer.sortingOrder = 2;
        _animator.SetBool("Dead", false);
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
        if (!other.CompareTag("Bullet") || !_isAlive) return;

        health -= other.GetComponent<Bullet>().damage;
        StartCoroutine(KnockBack());

        if (health > 0)
        {
            // hit
            _animator.SetTrigger("Hit");
        }
        else
        {
            // dead
            _isAlive = false;
            _collider.enabled = false;
            _rigidbody.simulated = false;
            _spriteRenderer.sortingOrder = 1; // 캐릭터 가리지 않기 위해 순서 변경
            _animator.SetBool("Dead", true);

            GameManager.Instance.killCnt++;
            GameManager.Instance.GetExp();
            
        }
    }

    IEnumerator KnockBack()
    {
        var knockBackSize = 3;
        //yield return null; // 1프레임 쉬기
        yield return _waitForFixedUpdate; // 다음 하나의 물리 프레임 딜레이
        Vector3 playerPos = GameManager.Instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        _rigidbody.AddForce(dirVec.normalized * knockBackSize, ForceMode2D.Impulse);
    }

    void Dead()
    {
        gameObject.SetActive(false);
    }
}
