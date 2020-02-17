using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 0f;
    public float moveDelay = 0.4f;
    public float maxSpeed = 2.5f;
    public float accelMagnitude = 200f;
    public float cursorMagnitude = 5f;

    public int startingHealth = 5;
    public int currentHealth;
    
    private float timer;
    private Vector3 movement;
    private Vector2 movement2D;
    private Rigidbody2D playerRigidbody2D;
    //private Animator anim;
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
    }

    /*private void Update()
    {
        transform.Rotate(new Vector3(0f, 0f, 10) * Time.deltaTime);
    }*/

    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (Input.GetButton("Fire1") && timer >= moveDelay)
        {
            timer = 0f;
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
        }        
        //Animating(h, v);
    }

    private void Move(float x, float y, float magnitude)
    {
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

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        //healthSlider.value = currentHealth;
        //playerAudio.Play();
        Debug.Log("Current health : " + currentHealth);

        if (currentHealth <= 0)
        {            
            Sob();
        }
    }

    
    void Sob()
    {
        //anim.SetTrigger("Sob");
        //playerAudio.clip = sobClip;
        //playerAudio.Play();
        //playerMovement.enabled = false;
        Recover();
    }
    
    void Recover()
    {
        //anim.SetTrigger("Sob");
        //playerAudio.clip = sobClip;
        //playerAudio.Play();
        currentHealth = startingHealth;
        //playerMovement.enabled = true;            
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
