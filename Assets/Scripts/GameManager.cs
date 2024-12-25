using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PoolManager poolManager;
    
    public float gameTime;
    public float gameOverTime = 2 * 10f;
    public float gameLevelChangeTime = 10f;
    
    public Player player;

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
}
