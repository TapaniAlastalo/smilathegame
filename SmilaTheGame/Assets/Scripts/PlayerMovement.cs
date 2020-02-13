using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 2f;
    public int startingHealth = 5;
    public int currentHealth;

    //private float timer;
    private Vector3 movement;
    private Rigidbody2D playerRigidbody2D;
    //private Animator anim;
    private float maxSprintDistance = 4.0f;

    private void Awake()
    {
        playerRigidbody2D = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
    }

    void Update()
    {
        transform.Rotate(new Vector3(0f, 0f, 10) * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        //timer += Time.deltaTime;
        if (Input.GetButton("Fire1")) //(isPressed)
        {
            Debug.Log("Mouse position called");
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Vector3.Distance(mousePos, playerRigidbody2D.position) > maxSprintDistance)
            {
                //playerRigidbody2D.position = playerRigidbody2D.position + (mousePos - playerRigidbody2D.position).normalized * maxSprintDistance;
                movement = (mousePos - playerRigidbody2D.position).normalized * maxSprintDistance * speed * Time.deltaTime;
                playerRigidbody2D.MovePosition(transform.position + movement);
                Debug.Log("Mouse position too far");
            }
            else
            {
                Debug.Log("Mouse position close enough");
                //playerRigidbody2D.position = mousePos;
                movement = (mousePos - playerRigidbody2D.position).normalized * speed * Time.deltaTime;
                playerRigidbody2D.MovePosition(transform.position + movement);
            }
        }
        //if (Input.GetButton("Fire1"))   // && timer >= timeBetweenBullets)
        //{
        else
        {
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");
            if (x != 0 || y != 0)
            {
                Move(x, y);
            }
        }
        //}        
        //Animating(h, v);
    }

    private void Move(float x, float y)
    {
        Debug.Log("Move to " + x + ", " + y);
        movement.Set(x, y, 0f);
        movement = movement.normalized * speed * Time.deltaTime;
        playerRigidbody2D.MovePosition(transform.position + movement);
    }

    /*private void Animating(float h, float v)
    {
        bool walking = h != 0f || v != 0f;
        anim.SetBool("IsWalking", walking);
    }*/

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        //healthSlider.value = currentHealth;
        //playerAudio.Play();
        Debug.Log("Current health : " + currentHealth);

        if (currentHealth <= 0)
        {
            currentHealth = startingHealth;
            //Sob();
        }
    }

    /*
    void Sob()
    {
        anim.SetTrigger("Sob");
        playerAudio.clip = sobClip;
        playerAudio.Play();
        playerMovement.enabled = false;
        Recover();
    }
    */

    /*
    void Recover()
    {
        anim.SetTrigger("Sob");
        playerAudio.clip = sobClip;
        playerAudio.Play();
        currentHealth = startingHealth;
        playerMovement.enabled = true;            
    } 
    */
}
