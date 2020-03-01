using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    // movement
    private float speed = 0f;
    public float moveDelay = 0.4f;
    public float maxSpeed = 1.5f;
    public float accelMagnitude = 140f;

    // health and damage
    public float startingHealth = 4f;
    private float currentHealth;
    private bool damaged;
    public float hitTolerance = 5.0f; 
    public int strength = 1;
    public Slider healthSlider;        

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
        EnemyManager.AddEnemy();
        currentHealth = startingHealth;
        healthSlider.value = currentHealth;
        damaged = false;
        healthSlider.gameObject.SetActive(false);
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
        if (collision.gameObject.tag.Equals("Player"))
        {
            AttackPlayer(collision.gameObject.GetComponent<Player>(), magnitude);
            // And wait for counter attack
        }
        else
        {            
            TakeDamage(magnitude);
        }
    }

    public void TakeDamage(float force)
    {
        if (force > hitTolerance)
        {
            // Show health slider when damaged
            if (damaged == false)
            {
                damaged = true;
                healthSlider.gameObject.SetActive(true);
            }
            // count the amount of damage caused
            int amount = (int)(force / hitTolerance);
            currentHealth -= amount;
            healthSlider.value = currentHealth;

            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }


    private void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        EnemyManager.RemoveEnemy(strength);
        Destroy(gameObject);
    }
    
    private void AttackPlayer(Player player, float magnitude)
    {
        // count the attack force
        float force = strength * magnitude;
        player.TakeDamage(force);
    }

    
}
