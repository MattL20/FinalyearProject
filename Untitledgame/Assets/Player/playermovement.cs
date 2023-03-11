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
     public Transform player;
    public bool isFlipped = false;
    //int direction = 0;
    public float LastX = 0;
    public float LastY = 0;

    public int maxHealth = 100;
    private int currentHealth;

    public HealthBar Hp;
    private int enemyAttDmg = 20;

    public GameObject Death;

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    

    Vector2 movementInput;
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        
        Hp.SetMaxHealth(maxHealth);
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
        Hp.SetHealth(currentHealth);

        //Debug.Log(movementInput.sqrMagnitude);
        //Debug.Log(direction);
    }

    void FixedUpdate()
    {
        //movement
        //
        

        if (movementInput != Vector2.zero){
                int count = rb.Cast(
                movementInput,
                movementFilter,
                castCollisions,
                MovementSpeed * Time.fixedDeltaTime + collisionOffset);
                if(count == 0){
            rb.MovePosition(rb.position + movementInput * MovementSpeed * Time.fixedDeltaTime);
        }
        //test
        }
      
    }
   
    void OnMove(InputValue movementValue){
        movementInput = movementValue.Get<Vector2>(); 
    }
    public void TakeDamage()
    {
       
            currentHealth -= enemyAttDmg;
        //animator.SetTrigger("TakeDmg");
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        // animator.SetTrigger("IsDead");
        Death.GetComponent<DeathScreenManager>().Died();
    }
    public void respawnPlayer(Vector3 pos){
        transform.position = pos;
        
    }
    public void resetHealth(){
        currentHealth = maxHealth;
    }
}
