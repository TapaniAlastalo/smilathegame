using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{    
    public const int firstScene = 1;
    public const int lastScene = 2;
    private static int currentScene = firstScene;

    void Start()
    {
        //currentScene = firstScene;        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private static void GameOver()
    {
        Debug.Log("Game Over!");
        // Appplication.LoadLevel("Level 01");
        //GameObject gameMenuObject = GameObject.FindGameObjectWithTag("GameMenu");
        //gameObject.enabled = true;
    }

    public static void NextLevel()
    {
        // Appplication.LoadLevel(Application.nextLevel);
        if(currentScene < lastScene)
        {
            currentScene++;
            Debug.Log("Next Scene!");
        }
        else
        {
            GameOver();
        }
        
    }

    public static void StartGame()
    {
        Debug.Log("First Scene!");
        //Appplication.LoadLevel(Application.loadedLevel);
    }

    /* NOT NEEDED AT THE MOMENT     
     * IEnumerator Release()
    {
        yield return new WaitForSeconds(releaseTime);
        GetComponent<SpringJoint2D>().enabled = false;
        this.enabled = false;

        yield return new WaitForSeconds(WaitForTurnTime);

        if(nextBall != null)
        {
            nextBall.SetActive(true);
        }
        else
        {
            Debug.Log("Game over");
            Enemy.EnemiesAlive = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }*/
}
