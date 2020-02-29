using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static int score = 0;
    private const int maxMultiplier = 1000;
    private const int minMultiplier = 10;

    private static float timer;
    private static float waitTime = 1.4f;
    
    private Text scoreText;

    private void Awake()
    {
        scoreText = GetComponent<Text>();
        timer = 0;
    }

    void Update()
    {
        timer += Time.deltaTime;
        scoreText.text = "" + score;
    }

    // Handle Score changes
    public static void Score(int points)
    {
        int multiplier = (maxMultiplier - (int)timer);
        //int multiplier = (int)(maxMultiplier - Time.time);
        if (multiplier < minMultiplier)
        {
            multiplier = minMultiplier;
        }
        //Debug.Log("Score + " + (multiplier * points * minMultiplier));
        score += multiplier * points * minMultiplier;       // tens are better than ones
    }

    // Handle level win
    public static void LevelWin()
    {
        StaticCoroutine.Start(WaitNow());        
    }

    private static void loadNextLevel()
    {
        SceneLoader.LoadNext();
    }

    // Reset scores
    public static void Reset()
    {
        score = 0;
    }
    
    static IEnumerator WaitNow()
    {
        yield return new WaitForSeconds(waitTime);
        loadNextLevel();
    }
}
