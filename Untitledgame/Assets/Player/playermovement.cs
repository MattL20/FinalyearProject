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
    public bool canmove;
    public ContactFilter2D movementFilter;
    public float collisionOffset = 0.05f;
     public Transform player;
    public bool isFlipped = false;
    int direction = 0;
    
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    

    Vector2 movementInput;
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        canmove = true;

    }

    // Update is called once per frame
    void Update(){
      //  animator.SetFloat("Horizontal",movementInput.x);
       // animator.SetFloat("Vertical",movementInput.y);
        animator.SetFloat("Speed",movementInput.sqrMagnitude);
        animator.SetInteger("Direction", direction);
        //Debug.Log(movementInput.sqrMagnitude);
    }

    void FixedUpdate()
    {
        //movement
        //
        
        if(canmove && movementInput != Vector2.zero){
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
        if(movementInput.x >=1f || movementInput.x<=-1f){
            direction = 1;
        }
        if(movementInput.y >=1f){
            direction = 2;
        }
        if(movementInput.y <=-1f){
            direction = 0;
        }
        flip();
        
        
        
    }
    public void flip(){
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;
         
    if(movementInput.x < 0 && isFlipped){
        transform.localScale = flipped;
        transform.Rotate(0f,180f,0f);
        isFlipped = false;
        //Debug.Log("false");
    }
    else if(movementInput.x > 0 && !isFlipped){
        transform.localScale = flipped;
        transform.Rotate(0f,180f,0f);
        isFlipped = true;
       // Debug.Log("True");
    }
    //Debug.Log(movementInput.x);
    }
    void OnMove(InputValue movementValue){
        movementInput = movementValue.Get<Vector2>(); 
    }
}
