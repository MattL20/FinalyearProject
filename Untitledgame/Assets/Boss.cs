using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Transform player;
    public bool isFlipped = false;
    public float speed = 3f;
    
    Rigidbody2D rb;
    Transform Target;
    Vector2 moveDirection;
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start() {
        Target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update() {
        if(Target){
            Vector3 direction = (Target.position-transform.position).normalized;
            moveDirection = direction;
        }
    }
    private void FixedUpdate() {
        if(Target){
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * speed;
        }
        LookAtPlayer();
    }
     public void LookAtPlayer(){
         Vector3 flipped = transform.localScale;
        flipped.z *= -1f;
         
    if(transform.position.x > player.position.x && isFlipped){
        transform.localScale = flipped;
        transform.Rotate(0f,180f,0f);
        isFlipped = false;
        //Debug.Log("false");
    }
    else if(transform.position.x < player.position.x && !isFlipped){
        transform.localScale = flipped;
        transform.Rotate(0f,180f,0f);
        isFlipped = true;
       // Debug.Log("True");
    }
     }
    // Start is called before the first frame update
    
}
