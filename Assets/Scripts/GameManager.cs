using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    
    [Header("game control")]
    public float gameTime;
    public float gameOverTime = 2 * 10f;
    public float gameLevelChangeTime = 10f;
    
    [Header("game object")]
    public PoolManager poolManager;
    public Player player;

    [Header("player info")]
    public int level;
    public int killCnt;
    public int exp;
    public int[] nextExp = { 3, 5, 10, 30, 60, 100, 150, 210, 280, 360, 450, 600 };

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        gameTime += Time.deltaTime;

        if (gameTime >= gameOverTime)
        {
            //gameTime = gameOverTime;
        }
    }

    public void GetExp()
    {
        exp++;

        if (exp >= nextExp[level])
        {
            level++;
            exp = 0;
        }
    }
}
