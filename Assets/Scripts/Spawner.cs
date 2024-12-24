using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    
    float _spawnTimer;
    public float spawnDelay;

    private void Awake()
    {
        spawnPoints = GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        _spawnTimer += Time.deltaTime;

        if (_spawnTimer >= spawnDelay)
        {
            Spawn();
            _spawnTimer = 0f;
        }
    }

    void Spawn()
    {
        GameObject enemy = GameManager.Instance.poolManager.GetObject(Random.Range(0, GameManager.Instance.poolManager.prefabs.Length));
        
        // 자기 자신이 0번, 자식 오브젝트 중에서만 선택되도록 하기 위함
        enemy.transform.position = spawnPoints[Random.Range(1, spawnPoints.Length)].position; 
    }
}
