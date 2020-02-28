using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public static int enemies;

    private Text text;

    private void Awake()
    {
        enemies = 0;
        text = GetComponent<Text>();
    }

    void Update()
    {
        text.text = "" + enemies;        
    }

    public static void AddEnemy()
    {
        enemies++;
    }

    public static void RemoveEnemy()
    {
        enemies--;
        CheckLevelWin();
    }

    public static void CheckLevelWin()
    {
        if (enemies <= 0)
        {
            SceneLoader.LoadNext();
        }
    }
}
