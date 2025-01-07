using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per; // 관통
    public float speed = 15f;

    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Init(float damage, int per, Vector3 dir)
    {
        this.damage = damage;
        this.per = per;

        if (per > -1)
        {
            // 원거리 무기인 경우
            _rigidbody2D.linearVelocity = dir * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 관통 로직에서 제외되는 조건
        if(!other.CompareTag("Enemy") || per == -1) return;

        per--;

        if (per == -1)
        {
            _rigidbody2D.linearVelocity = Vector2.zero; // 재사용 대비해 속도 미리 리셋
            gameObject.SetActive(false);
        }
    }

    // 맵 밖으로 나가면 총알 비활성화
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Area"))
        {
            _rigidbody2D.linearVelocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }
}
