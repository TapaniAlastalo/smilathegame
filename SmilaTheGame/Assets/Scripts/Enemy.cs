using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 4f;
    public float hitTolerance = 5.0f; 
    public int strength = 1;

    public float speed = 0f;
    public float moveDelay = 0.4f;
    public float maxSpeed = 1.5f;
    public float accelMagnitude = 140f;    

    public GameObject deathEffect;

    private float timer;
    private Vector3 movement;
    private Vector2 movement2D;
    private Rigidbody2D enemyRigidbody2D;

    Player player;


    private void Awake()
    {
        enemyRigidbody2D = GetComponent<Rigidbody2D>();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if(playerObject != null)
        {
            player = playerObject.GetComponent<Player>();
        }        
    }

    void Start()
    {
        EnemyManager.AddEnemy();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= moveDelay)
        {
            timer = 0f;            
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
        float magnitude = collision.relativeVelocity.magnitude;
        Debug.Log("Hit magnitude: " + magnitude);
        if (magnitude > hitTolerance)
        {
            health--;
            Debug.Log("Hit Taken, Health: " + health);                
            if (health <= 0f)
            {
                Die();
            }
        }
        if (collision.gameObject.tag.Equals("Player"))
        {
            AttackPlayer(magnitude);
        }
    }

    
    private void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        EnemyManager.RemoveEnemy(strength);
        Destroy(gameObject);
    }
    
    private void AttackPlayer(float magnitude)
    {
        // count the force of attack
        int force = (int)(strength * magnitude);
        player.TakeDamage(force);
    }

    
}
