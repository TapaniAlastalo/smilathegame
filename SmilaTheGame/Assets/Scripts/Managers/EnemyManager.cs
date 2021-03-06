﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    private static int enemies;
    private Text enemyText;

    private void Awake()
    {
        enemies = 0;
        enemyText = GetComponent<Text>();
    }

    void Update()
    {
        enemyText.text = "" + enemies;        
    }

    public static void AddEnemy()
    {
        enemies++;
    }

    public static void RemoveEnemy(int strength)
    {
        enemies--;
        Score(strength);
        CheckLevelWin();
    }

    private static void Score(int strength)
    {
        ScoreManager.Score(strength);
    }

    private static void CheckLevelWin()
    {
        if (enemies <= 0)
        {
            ScoreManager.LevelWin();
        }
    }
}
