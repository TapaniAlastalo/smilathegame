using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 4f;
    public float hitTolerance = 2f;

    public float speed = 0f;
    public float moveDelay = 0.5f;
    public float maxSpeed = 1.2f;
    public float accelMagnitude = 100f;

    public static int EnemiesAlive = 0;
    public GameObject deathEffect;

    private float timer;
    private Vector3 movement;
    private Vector2 movement2D;
    private Rigidbody2D enemyRigidbody2D;


    private void Awake()
    {
        enemyRigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        EnemiesAlive++;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= moveDelay)
        {
            timer = 0f;
            //Vector2 randomPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);            
            var x = Random.Range(-1000, 1000);
            var y = Random.Range(-1000, 1000);
            Move(x, y, accelMagnitude);
        }
    }

    private void Move(float x, float y, float magnitude)
    {
        movement2D.Set(x, y);
        speed = Mathf.Min(speed + accelMagnitude * Time.deltaTime, maxSpeed);
        // Normalise the movement vector and make it proportional to the speed per second.
        movement2D = movement2D.normalized * speed * Time.deltaTime;
        enemyRigidbody2D.AddForce(movement2D * magnitude, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if (collision.relativeVelocity.magnitude > hitTolerance)
            {
                health--;
                Debug.Log("Hit Taken, Healt: " + health);
                if (health <= 0)
                {
                    Die();
                }
            }
        }
    }

    
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
}
