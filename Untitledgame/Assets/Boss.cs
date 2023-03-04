using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Transform player;
    public bool isFlipped = true;
    public float speed = 3f;
    public Transform[] waypoints;
    private int waypointIndex = 0;
    public Animator animator;
    public bool canMove = false;
    public float AgroRange = 5f;
    public float AttRange = 3f;

    private float _waitTime = 1f; // in seconds
    private float _waitCounter = 0f;
    private bool _waiting = false;

    private float _waitTime2 = 1.5f; // in seconds
    private float _waitCounter2 = 0f;
    private bool _waiting2 = false;

    private bool hasAttacked = false;

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
        float dist = Vector2.Distance(transform.position,player.position);
       
        
        if(dist<AgroRange&&!hasAttacked&&dist>AttRange){
            Agro();
        }
        if(dist<AttRange&&!hasAttacked){
            canMove = false;
            StopAgro();
            Attack(); 
        }else if(hasAttacked){
            hasAttacked = false;
            canMove = true;
            
            //rb.velocity = new Vector2(waypoints[waypointIndex].position.x, waypoints[waypointIndex].position.y) * speed;
           // Debug.Log("After attack "+ speed);
        }
        //Debug.Log("hasAttacked: " + hasAttacked);
        //Debug.Log("canMove: " +canMove);
        if (_waiting2)
        {
               Debug.Log("Waiting");
               
                _waitCounter2 += Time.deltaTime;
            if (_waitCounter2 < _waitTime2)
                return;
                speed = 6;
            _waiting2 = false;
            
        }
        
        
        if (canMove)
        {

            Move();
            
        }
        
    }
    private void FixedUpdate() {
       // if(Target){
       //     rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * speed;
       // }
        //LookAtPlayer();
      
        
                   
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
            Flip();
          //  Debug.Log("IsMoving");
        // If Enemy didn't reach last waypoint it can move
        // If enemy reached last waypoint then it stops
        animator.SetBool("IsMoving", true);
            if (waypointIndex == 2 || waypointIndex == 5 || waypointIndex == 8 || waypointIndex == 11 ||waypointIndex == 14 ||waypointIndex == 17 ||waypointIndex == 20 ||waypointIndex == 23)
            if (_waiting)
        {
                animator.SetBool("IsMoving", false);
                _waitCounter += Time.deltaTime;
            if (_waitCounter < _waitTime)
                return;
            _waiting = false;
           // Debug.Log(waypointIndex);
        }
            Transform wp = waypoints[waypointIndex];
            if (Vector3.Distance(transform.position, wp.position) < 0.3f)
            {
                transform.position = wp.position;
            _waitCounter = 0f;
            _waiting = true;
                // Move Enemy from current waypoint to the next one
                // using MoveTowards method
                if(waypointIndex==waypoints.Length-1){
                    waypointIndex = 0;
                }
                else{
                    waypointIndex = (waypointIndex + 1);
                }
                
               
            }
        else
        {
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
        }
        public void Flip(){
            Vector3 flipped = transform.localScale;
        flipped.z *= -1f;
        if(isFlipped&&waypoints[waypointIndex].transform.position.x<transform.position.x){
        transform.localScale = flipped;
        transform.Rotate(0f,180f,0f);
        isFlipped = false;
        }else if(!isFlipped&&waypoints[waypointIndex].transform.position.x>transform.position.x){
            transform.localScale = flipped;
        transform.Rotate(0f,180f,0f);
        isFlipped = true;
        }
        }
        public void Agro(){
            LookAtPlayer();
             transform.position = Vector2.MoveTowards(transform.position,
                   player.transform.position,
                   speed * Time.deltaTime);

        }
        public void StopAgro(){
            LookAtPlayer();
            speed = 0;
           // Debug.Log(rb.velocity);
        }
        public void Attack(){
           // Debug.Log("Attacked");
            //LookAtPlayer();
            animator.SetTrigger("Attack");
            _waiting2 = true;
            hasAttacked = true;
        }
    
   

}
