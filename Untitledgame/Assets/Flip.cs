using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class Flip : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    public bool isFlipped = false;
    Vector2 movementInput;
    public float MovementSpeed = 5f;
    public Rigidbody2D rb;
   // public Animator animator;
    public ContactFilter2D movementFilter;
    public float collisionOffset = 0.05f;
    
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    public void FixedUpdate() {
        if(movementInput != Vector2.zero){
                int count = rb.Cast(
                movementInput,
                movementFilter,
                castCollisions,
                MovementSpeed * Time.fixedDeltaTime + collisionOffset);
            
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
