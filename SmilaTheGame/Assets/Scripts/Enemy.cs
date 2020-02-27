using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 4f;
    public float hitTolerance = 2f; 
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
        if (collision.gameObject.tag.Equals("Player"))
        {
            if (collision.relativeVelocity.magnitude > hitTolerance)
            {
                health--;
                Debug.Log("Hit Taken, Health: " + health);                
                if (health <= 0f)
                {
                    Die();
                }
                else
                {                    
                    Attack();
                }
            }
        }
    }

    
    private void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        EnemyManager.RemoveEnemy();
        Score();        
        Destroy(gameObject);
    }
    
    private void Attack()
    {
        player.TakeDamage(strength);
    }

    private void Score()
    {
        ScoreManager.score += strength;
    }
}
