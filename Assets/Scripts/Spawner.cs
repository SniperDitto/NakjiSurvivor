using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public SpawnData[] spawnData;
    
    int level;
    float _spawnTimer;

    private void Awake()
    {
        spawnPoints = GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        _spawnTimer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.Instance.gameTime/GameManager.Instance.gameLevelChangeTime), spawnData.Length-1);

        if ( _spawnTimer > spawnData[level].spawnInterval)
        {
            Spawn();
            _spawnTimer = 0f;
        }
    }

    void Spawn()
    {
        GameObject enemy = GameManager.Instance.poolManager.GetObject(0);
        
        // 자기 자신이 0번, 자식 오브젝트 중에서만 선택되도록 하기 위함
        enemy.transform.position = spawnPoints[Random.Range(1, spawnPoints.Length)].position; 
        enemy.GetComponent<Enemy>().Init(spawnData[level]);
    }
}

[Serializable]
public class SpawnData
{
    public int spriteType;
    public float spawnInterval;
    public int health;
    public float speed;
}