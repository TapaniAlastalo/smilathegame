﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // movement
    private float speed = 0f;
    public float moveDelay = 0.4f;
    public float maxSpeed = 2.5f;
    public float accelMagnitude = 200f;
    public float cursorMagnitude = 5f;

    // hit and strength
    public float hitTolerance = 5.0f;
    public int strength = 1;

    // health
    public int startingHealth = 5;
    private int currentHealth;
    public Slider healthSlider;    
    
    private float timer;
    private bool paralyzed;
    public float paralyzeDelay = 0.9f;

    private Vector3 movement;
    private Vector2 movement2D;
    private Rigidbody2D playerRigidbody2D;

    // damage
    public Image damageImage;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.4f);
    public AudioClip damageClip;

    //private Animator anim;
    //AudioSource playerAudio;
    //private float maxSprintDistance = 4.0f;

    private void Awake()
    {
        playerRigidbody2D = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
    }

    private void Start()
    {
        timer = 5f;
        currentHealth = startingHealth;        
        paralyzed = false;
        healthSlider.value = currentHealth;
    }

    /*private void Update()
    {
        transform.Rotate(new Vector3(0f, 0f, 10) * Time.deltaTime);
    }*/

    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (paralyzed)
        {
            Recover();            
        }
        else
        {
            if (Input.GetButton("Fire1") && timer >= moveDelay)
            {                
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                movement = (mousePos - playerRigidbody2D.position);
                Move(movement.x, movement.y, accelMagnitude);
            }
            else
            {
                float x = Input.GetAxisRaw("Horizontal");
                float y = Input.GetAxisRaw("Vertical");
                if (x != 0 || y != 0)
                {
                    Move(x, y, cursorMagnitude);
                }
                else
                {
                    Recover();
                }
            }
        }
        //Animating(h, v);
    }

    private void Move(float x, float y, float magnitude)
    {
        timer = 0f;
        movement2D.Set(x, y);
        speed = Mathf.Min(speed + accelMagnitude * Time.deltaTime, maxSpeed);
        // Normalise the movement vector and make it proportional to the speed per second.
        movement2D = movement2D.normalized * speed * Time.deltaTime;
        // Move the player to it's current position plus the movement.
        playerRigidbody2D.AddForce(movement2D * magnitude, ForceMode2D.Impulse);
    }

    /*private void Animating(float h, float v)
    {
        bool walking = h != 0f || v != 0f;
        anim.SetBool("IsWalking", walking);
    }*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float magnitude = collision.relativeVelocity.magnitude;
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            AttackEnemy(collision.gameObject.GetComponent<Enemy>(), magnitude);
            // Wait for counter attack
        }
        else
        {            
            TakeDamage(magnitude);
        }        
    }

    public void TakeDamage(float force)
    {
        if(force > hitTolerance)
        {
            // count the amount of damage caused
            int amount = (int)(force / hitTolerance);
            currentHealth -= amount;
            healthSlider.value = currentHealth;
            //playerAudio.Play();

            if (currentHealth <= 0)
            {
                Paralyze();
            }
        }        
    }

    private void AttackEnemy(Enemy enemy, float magnitude)
    {
        // count the attack force
        float force = strength * magnitude;
        enemy.TakeDamage(force);
    }


    void Paralyze()
    {
        //anim.SetTrigger("Sob");
        //playerAudio.clip = sobClip;
        //playerAudio.Play();
        timer = 0f;
        paralyzed = true;
        damageImage.color = flashColour;
    }
    
    void Recover()
    {        
        if (timer >= paralyzeDelay)
        {
            timer = 0f;
            currentHealth++;
            healthSlider.value = currentHealth;
            if (paralyzed)
            {
                if (currentHealth == startingHealth)
                {
                    damageImage.color = Color.clear;
                    paralyzed = false;
                }
                else
                {
                    damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
                }
            }
        }
        //anim.SetTrigger("Sob");
        //playerAudio.clip = sobClip;
        //playerAudio.Play();        
    }
}
