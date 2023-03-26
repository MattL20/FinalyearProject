using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class playermovement : MonoBehaviour
{
    public float MovementSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;
    public ContactFilter2D movementFilter;
    public float collisionOffset = 0.05f;
    public float LastX = 0;
    public float LastY = 0;


    public int maxHealth = 100;
    private int currentHealth;

    public GameObject Death;
    public GameObject Boss;

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    public AudioSource walking;
    public AudioSource BossMusic;
    public AudioSource getHit;

    public static bool Alive = true;
    public static bool isBossAlive;
    Vector2 movementInput;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update(){
        animator.SetFloat("Horizontal",movementInput.x);
        animator.SetFloat("Vertical",movementInput.y);
        animator.SetFloat("Speed",movementInput.sqrMagnitude);
        if (movementInput.x >= 0.1f || movementInput.x <= -0.1f || movementInput.y >= 0.1f || movementInput.y <= -0.1f)
        {
            LastX = movementInput.x;
            LastY = movementInput.y;
            animator.SetFloat("LastX", LastX);
            animator.SetFloat("LastY", LastY);
        }

        isBossAlive = Boss.GetComponent<Boss>().getAlive();
    }

    void FixedUpdate()
    {
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
    
    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;
        getHit.Play();
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Alive = false;
        Death.GetComponent<DeathScreenManager>().Died();
    }
    public void respawnPlayer(Vector3 pos){
        transform.position = pos;
        
    }
    public void resetHealth(){
        currentHealth = maxHealth;
    }
    public int getHealth()
    {
        return currentHealth;
    }
    public int getMaxHealth()
    {
        return maxHealth;
    }
    public void bossMusicStart()
    {
        GameObject[] normal = GameObject.FindGameObjectsWithTag("GameMusicNormal");
        foreach(var n in normal)
        {
            Destroy(n);
        }
        BossMusic.Play();
    }
    public bool getAlive()
    {
        return Alive;
    }
    public void Retry()
    {
        Alive = true;
    }
    public bool getIsBossAlive()
    {
        return isBossAlive;
    }
}
