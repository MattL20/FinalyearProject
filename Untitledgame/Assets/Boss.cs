using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Transform player;
    public bool isFlipped = false;
    public float speed = 3f;
    public Transform[] waypoints;
    private int waypointIndex = 0;

    Rigidbody2D rb;
    Transform Target;
    Vector2 moveDirection;
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start() {
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        transform.position = waypoints[waypointIndex].transform.position;
    }
    private void Update() {
        //  if(Target){
        //      Vector3 direction = (Target.position-transform.position).normalized;
        //     moveDirection = direction;
        // }
        Move();
    }
    private void FixedUpdate() {
       // if(Target){
       //     rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * speed;
       // }
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
     public void Move()
        {
            // If Enemy didn't reach last waypoint it can move
            // If enemy reached last waypoint then it stops
            if (waypointIndex <= waypoints.Length - 1)
            {

                // Move Enemy from current waypoint to the next one
                // using MoveTowards method
                transform.position = Vector2.MoveTowards(transform.position,
                   waypoints[waypointIndex].transform.position,
                   speed * Time.deltaTime);

                // If Enemy reaches position of waypoint he walked towards
                // then waypointIndex is increased by 1
                // and Enemy starts to walk to the next waypoint
                if (transform.position == waypoints[waypointIndex].transform.position)
                {
                waypointIndex += 1;
                }
            }
        else
        {
            waypointIndex = 0;
        }
        }
    
   

}
