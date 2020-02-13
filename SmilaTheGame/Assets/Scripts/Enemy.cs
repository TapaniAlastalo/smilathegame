using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 2f;

    public static int EnemiesAlive = 0;

    // Start is called before the first frame update
    void Start()
    {
        EnemiesAlive++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude > health)
        {
            //Die();
        }
        Debug.Log(collision.relativeVelocity.magnitude);
    }

    /*
    private void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        EnemiesAlive--;
        if (EnemiesAlive <= 0)
        {
            //SceneManager.
            Debug.Log("Level Won");
        }
        Destroy(gameObject);
    }
    */
}
