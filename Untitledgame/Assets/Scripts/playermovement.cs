using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
//Used to contol the players movement
public class playermovement : MonoBehaviour
{
    public float MovementSpeed = 5f;//Speed of the player
    public Rigidbody2D rb;//Reference to the player rigidbidy
    public Animator animator;//Reference to the player animator
    //Variables to check collisions druing movement
    public ContactFilter2D movementFilter;
    public float collisionOffset = 0.05f;
    //Hold the players last direction faced
    public float LastX = 0;
    public float LastY = 0;

    //health variables
    public int maxHealth = 100;
    private int currentHealth;

    //reference to the death screen UI and the boss game object
    public GameObject Death;
    public GameObject Boss;
    //holds all collsions during movement
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    //All sound effects
    public AudioSource walking;
    public AudioSource BossMusic;
    public AudioSource getHit;

    public static bool Alive = true;//true if the player is alive
    public static bool isBossAlive;//true if the boss is alive
    Vector2 movementInput;
    //Called first finds the player rigidbody and sets current health
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update(){
        //Sets variables in the animator to play the appropriate animation
        animator.SetFloat("Horizontal",movementInput.x);
        animator.SetFloat("Vertical",movementInput.y);
        animator.SetFloat("Speed",movementInput.sqrMagnitude);
        if (movementInput.x >= 0.1f || movementInput.x <= -0.1f || movementInput.y >= 0.1f || movementInput.y <= -0.1f)
        {
            //Sets last direction variables in the animator to play the appropriate idle animation
            LastX = movementInput.x;
            LastY = movementInput.y;
            animator.SetFloat("LastX", LastX);
            animator.SetFloat("LastY", LastY);
        }
        //Check if the boss is alive
        isBossAlive = Boss.GetComponent<Boss>().getAlive();
    }

    void FixedUpdate()
    {
        //moves the player depending on input
        if (movementInput != Vector2.zero){
                int count = rb.Cast(
                movementInput,
                movementFilter,
                castCollisions,
                MovementSpeed * Time.fixedDeltaTime + collisionOffset);
                if(count == 0){
                rb.MovePosition(rb.position + movementInput * MovementSpeed * Time.fixedDeltaTime);
                }
        }
      
    }
   //take in movement input and controls when the walking sound effect plays
    void OnMove(InputValue movementValue){
        movementInput = movementValue.Get<Vector2>();
        if (movementInput != Vector2.zero&& walking.isPlaying == false)
        {
            walking.Play();
        }else if(movementInput == Vector2.zero&& walking.isPlaying == true)
        {
            walking.Stop();
        }
    }
    //take in a damage value and take away from currenthealth
    //if health reaches 0 call die function
    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;
        getHit.Play();
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    //set alive to false and show the death screen
    void Die()
    {
        Alive = false;
        Death.GetComponent<DeathScreenManager>().Died();
    }
    //used to move the player after they retry after a death
    public void respawnPlayer(Vector3 pos){
        transform.position = pos;
        
    }
    //resets health to max health
    public void resetHealth(){
        currentHealth = maxHealth;
    }
    //getter
    public int getHealth()
    {
        return currentHealth;
    }
    //getter
    public int getMaxHealth()
    {
        return maxHealth;
    }
    //play the boss fight music
    public void bossMusicStart()
    {
        GameObject[] normal = GameObject.FindGameObjectsWithTag("GameMusicNormal");
        foreach(var n in normal)
        {
            Destroy(n);
        }
        BossMusic.Play();
    }
    //getter
    public bool getAlive()
    {
        return Alive;
    }
    //used for retrying to set alive to true again
    public void Retry()
    {
        Alive = true;
    }
    //returns if the boss is alive
    public bool getIsBossAlive()
    {
        return isBossAlive;
    }
}
