using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Boss : MonoBehaviour
{
    public Transform player;
    public bool isFlipped = true;
    public float speed;
    public Transform[] waypoints;
    private int waypointIndex = 0;
    public Animator animator;
    public bool canMove = false;
    public float AgroRange = 5f;
    public float AttRange = 3f;
    private bool Alive = true;
    private static float maxSpeedS1 = 6f;
    

    private float _waitTime = 1f; 
    private float _waitCounter = 0f;
    private bool _waiting = false;

    private float _waitTime2 = 1f; 
    private float _waitCounter2 = 0f;
    private bool _waiting2 = false;

    private float dmgwaitTime = 5f; 
    private float dmgwaitCounter = 0f;
    private static bool dmgwaiting = false;

    //public HealthBar Hp;

    public AudioSource punch;

    private bool hasAttacked = false;
    private bool InAgro = false;

    public int maxHealth = 100;
    private static int currentHealth;
   

    public Transform AttackPointRight;
    //public Transform AttackPointUp;
    public float attackRange = 1.2f;
    public LayerMask playerLayer;
    private Collider2D[] hitPlayer;
    private Collider2D[] hitPlayerAbove;

    private static GameObject BBarrel;

    private static bool Fixing = false;

    private static int BarrelCount = 8;

    public static bool isPlayerAlive;

    
    Rigidbody2D rb;
    Transform Target;
    Vector2 moveDirection;
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        
        currentHealth = maxHealth;
       // Debug.Log("On Awake" + currentHealth);
    }
    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
       // Hp.SetMaxHealth(maxHealth);
        
    }
    private void Update() {
        
        float dist = Vector2.Distance(transform.position,player.position);
        //Debug.Log("Update Fixing " + Fixing);
        //Debug.Log("BarrelCount " + BarrelCount );
        if (BarrelCount <= 3)
        {
            maxSpeedS1 = 9;
        }else if(BarrelCount <= 6)
        {
            maxSpeedS1 = 7;
        }
        
        if(Alive&&!Fixing){
            if (dist < AgroRange && !hasAttacked)
            {
                Agro();
                if (dist < AttRange && !hasAttacked)
                {
                    
                    // StopAgro();
                Attack();
                    
                    
                    _waitCounter2 = 0f;
                    
                }
            }
            if(dist > AgroRange && InAgro)
            {
                speed = maxSpeedS1;
                Move();
            }
            if (hasAttacked)
            {
    
                if (_waiting2)
                {
                // Debug.Log("HERE ");
                // Debug.Log("Wait Counter " + _waitCounter2);

                    _waitCounter2 += Time.deltaTime;
                    if (_waitCounter2 < _waitTime2)
                        return;
                    speed = maxSpeedS1;
                    hasAttacked = false;
                    canMove = true;
                    _waiting2 = false;
                    animator.SetBool("IsMoving", true);
                // Debug.Log("Done Waiting");
                }
            }
            if (canMove)
            {
                Move();    
            }
            isPlayerAlive = player.GetComponent<playermovement>().getAlive();
            //Debug.Log("Player is alive? " + isPlayerAlive);
        }
        if(Fixing){

            Fix();
            if (dmgwaiting)
            {
                
                dmgwaitCounter += Time.deltaTime;
                if (dmgwaitCounter < dmgwaitTime)
                    return;
                animator.SetBool("IsMoving", true);
                speed = maxSpeedS1;
                
                    Fixing = false;
                
                dmgwaitCounter = 0;
                dmgwaiting = false;
            }
            
        }
        
        //Debug.Log("Fixing Outside= " + Fixing);
        //Debug.Log(Fixing);

    }
    private void FixedUpdate() {             
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
            //Debug.Log("Speed" + speed);
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
        if(isFlipped&&waypoints[waypointIndex].transform.position.x<transform.position.x&&!Fixing){
            transform.localScale = flipped;
            transform.Rotate(0f,180f,0f);
            isFlipped = false;
        }else if(!isFlipped&&waypoints[waypointIndex].transform.position.x>transform.position.x&&!Fixing){
            transform.localScale = flipped;
            transform.Rotate(0f,180f,0f);
            isFlipped = true;
        }else if(Fixing&&isFlipped&&transform.position.x>BBarrel.transform.position.x){
            transform.localScale = flipped;
            transform.Rotate(0f,180f,0f);
            isFlipped = false;
        }else if(Fixing&&!isFlipped&&transform.position.x<BBarrel.transform.position.x){
            transform.localScale = flipped;
            transform.Rotate(0f,180f,0f);
            isFlipped = true;
        }
        }
        public void Agro(){
         LookAtPlayer();
        //Debug.Log("In Agro" + speed);
        InAgro = true;
        animator.SetBool("IsMoving", true);
        canMove = false;
        Vector3 direction = (player.transform.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x, direction.y) * speed;
        //transform.position = Vector3.MoveTowards(transform.position,
        //           player.position,
        //           speed * Time.deltaTime);

        }
    
        public void Attack(){
        // Debug.Log("Attacked");
        //LookAtPlayer();
        InAgro = false;
        //Debug.Log("In Attack");
        speed = 0;
        rb.velocity = Vector2.zero;
        animator.SetTrigger("Attack");
        animator.SetBool("IsMoving", false);
        _waiting2 = true;
        hasAttacked = true;

        hitPlayer = Physics2D.OverlapCircleAll(AttackPointRight.position, attackRange, playerLayer);
       // hitPlayerAbove = Physics2D.OverlapCircleAll(AttackPointUp.position, attackRange/2, playerLayer);
        foreach (Collider2D p in hitPlayer)
        {
            p.GetComponent<playermovement>().Invoke("TakeDamage", 0.5f);
        }
        //foreach (Collider2D p in hitPlayerAbove)
        //{
        //    p.GetComponent<playermovement>().Invoke("TakeDamage", 0.5f);
        //}
    }
    public void TakeDamage(int dmg)
    {

        currentHealth -= dmg;
        animator.SetTrigger("TakeDmg");
        punch.Play();
        if (currentHealth<=0)
        {
            Die();
            
            Alive = false;
        }
       // Debug.Log("on take damage"+ currentHealth);
        //Hp.SetHealth(currentHealth);
    }

    
    void Die()
    {
        animator.SetTrigger("IsDead");

    }
    public void Fix(){
        Flip();
        animator.SetBool("IsMoving", true);
        dmgwaiting = true;
        float dist = Vector2.Distance(transform.position, BBarrel.transform.position);
        // Debug.Log("BBarrel = " + BBarrel.transform.position);
        // Debug.Log("dist= " + dist);
        // Debug.Log("Speed= " + speed);

        if(dist>=1f){
            transform.position = Vector2.MoveTowards(transform.position,
                   BBarrel.transform.position,
                   speed * Time.deltaTime);
        }       
        
        if(dist<=1f){
            animator.SetBool("IsMoving", false);
            speed = 0;
        }
                   //Debug.Log(BBarrel.position.x);
    }
    public  void setFix(){
            Fixing = true;
            //canMove = false;
        
        
       
        //Debug.Log("Fixing " +Fixing + "Fixing2 " + Fixing2+ "Fixing3 " + Fixing3+ "Fixing4 " +Fixing4+  "Fixing5 " + Fixing5+ "Fixing6 " + Fixing6+ "Fixing7 " +Fixing7+ "Fixing8 " +Fixing8);
    }
    public void setBarrel(GameObject x){
        BBarrel = x;
        
    }
    public void countBarrel()
    {
        BarrelCount = BarrelCount -1;
        //Debug.Log("BarrelCount inside " + BarrelCount );
    }
    void OnDrawGizmosSelected()
    {
        if (AttackPointRight == null)
            return;
        
        Gizmos.DrawWireSphere(AttackPointRight.position, attackRange);
      //  Gizmos.DrawWireSphere(AttackPointUp.position, attackRange/2);
      
    }
    public int getHealth()
    {
        //Debug.Log(currentHealth);
        return currentHealth;
    }
    public int getMaxHealth()
    {
        return maxHealth;
    }
    public bool getIsPlayerAlive()
    {
        return isPlayerAlive;
    }


}
