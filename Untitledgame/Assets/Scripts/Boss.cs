using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Boss : MonoBehaviour
{
    //Reference to the player
    public Transform player;
    public bool isFlipped = true;//if the boss is flipped
    public float speed;// Current speed of the boss movement
    public Transform[] waypoints;// Patrolling path
    private int waypointIndex = 0;//Current target waypoint
    public Animator animator;//Reference to the boss animator
    public bool canMove = false;//If the boss can move or not
    public float AgroRange = 5f;//Range of the boss chasing the player
    public float AttRange = 3f;//Range of the boss attacking the player
    private bool Alive = true;
    //the max speed of the boss
    private static float maxSpeedS1 = 6f;
    //How much damage the boss does
    private int AttDmg = 25;

    //Timer variables
    private float _waitTime = 1f; 
    private float _waitCounter = 0f;
    private bool _waiting = false;

    private float _waitTime2 = 1f; 
    private float _waitCounter2 = 0f;
    private bool _waiting2 = false;

    private float dmgwaitTime = 5f; 
    private float dmgwaitCounter = 0f;
    private static bool dmgwaiting = false;
    //Audio for getting hit
    public AudioSource punch;

    private bool hasAttacked = false;
    private bool InAgro = false;
    //Health variables
    public int maxHealth = 100;
    private static int currentHealth;
   
    //Attacking variables
    public Transform AttackPointRight;
    public float attackRange = 1.2f;
    public LayerMask playerLayer;
    private Collider2D[] hitPlayer;
    //Reference to the most recent broken barrels
    private static GameObject BBarrel;
    private static int BarrelCount = 8;

    private static bool Fixing = false;
    //Check if the player is alive
    public static bool isPlayerAlive;

    
    Rigidbody2D rb;
    Transform Target;
    Vector2 moveDirection;
    //Called once before start frame and before start
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        
        currentHealth = maxHealth;
    }
    //Called once before start frame
    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        BarrelCount = 8;
        Alive = true;
    }
    //Called every frame
    private void Update() {
        //distance betwenn boss and player
        float dist = Vector2.Distance(transform.position,player.position);
        //speed is adjusted depending on the amount of variables left
        if (BarrelCount <= 3)
        {
            maxSpeedS1 = 9;
        }else if(BarrelCount <= 6)
        {
            maxSpeedS1 = 7;
        }
        //if the boss is not trying to "Fix" a barrel
        if(Alive&&!Fixing){
            //if the player is close enough to chase and is not attacking
            if (dist < AgroRange && !hasAttacked)
            {
                Agro();
                //attack if close enough
                if (dist < AttRange && !hasAttacked)
                {
                    Attack(); 
                    _waitCounter2 = 0f;
                }
            }
            //if the player escapes the range before the boss attacks return to patrolling
            if(dist > AgroRange && InAgro)
            {
                speed = maxSpeedS1;
                Move();
            }
            //Wait just after attacking before the boss can move again
            if (hasAttacked)
            {
                if (_waiting2)
                {
                    _waitCounter2 += Time.deltaTime;
                    if (_waitCounter2 < _waitTime2)
                        return;
                    speed = maxSpeedS1;
                    hasAttacked = false;
                    canMove = true;
                    _waiting2 = false;
                    animator.SetBool("IsMoving", true);
                }
            }
            //If the boss is allowed move, patrol
            if (canMove)
            {
                Move();    
            }
            //check if the player is alive
            isPlayerAlive = player.GetComponent<playermovement>().getAlive();
        }
        //If a broken barrel has just spawned move the player call the fix movement then wait 5 seconds
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
    }
    //Flip the boss sprites to face the player
     public void LookAtPlayer(){
         Vector3 flipped = transform.localScale;
        flipped.z *= -1f;
         
    if(transform.position.x > player.position.x && isFlipped){
        transform.localScale = flipped;
        transform.Rotate(0f,180f,0f);
        isFlipped = false;
 
    }
    else if(transform.position.x < player.position.x && !isFlipped){
        transform.localScale = flipped;
        transform.Rotate(0f,180f,0f);
        isFlipped = true;
    }
     }
    //Patrolling, walk along the path moving to each waypoint one by one and when reaching the end start again
     public void Move()
        {
            Flip();
            animator.SetBool("IsMoving", true);
            if (waypointIndex == 2 || waypointIndex == 5 || waypointIndex == 8 || waypointIndex == 11 ||waypointIndex == 14 ||waypointIndex == 17 ||waypointIndex == 20 ||waypointIndex == 23)
            if (_waiting)
        {
                rb.velocity = Vector2.zero;
                animator.SetBool("IsMoving", false);
                _waitCounter += Time.deltaTime;
            if (_waitCounter < _waitTime)
                return;
            _waiting = false;
        }
            Transform wp = waypoints[waypointIndex];
            if (Vector3.Distance(transform.position, wp.position) < 0.3f)
            {
                transform.position = wp.position;
                rb.velocity = Vector2.zero;
                _waitCounter = 0f;
                _waiting = true;
                if(waypointIndex==waypoints.Length-1){
                    waypointIndex = 0;
                }
                else{
                    waypointIndex = (waypointIndex + 1);
                }
                
               
            }
        else
        {
                Vector3 direction = (waypoints[waypointIndex].transform.position - transform.position).normalized;
                rb.velocity = new Vector2(direction.x, direction.y) * speed;
                if (transform.position == waypoints[waypointIndex].transform.position)
                {
                waypointIndex += 1;
                }
        }
        }
    //flip the boss sprites
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
    //Chasing the player movement, track player and constantly move towards the player
        public void Agro(){
         LookAtPlayer();
        InAgro = true;
        animator.SetBool("IsMoving", true);
        canMove = false;
        Vector3 direction = (player.transform.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x, direction.y) * speed;
        }
    //Stop moving, attack the player
    //check for overlaps in the boss attackrange and the player hit box and apply damage to any player hit
        public void Attack(){
        InAgro = false;
        speed = 0;
        rb.velocity = Vector2.zero;
        animator.SetTrigger("Attack");
        animator.SetBool("IsMoving", false);
        _waiting2 = true;
        hasAttacked = true;
        hitPlayer = Physics2D.OverlapCircleAll(AttackPointRight.position, attackRange, playerLayer);
        foreach (Collider2D p in hitPlayer)
        {
            StartCoroutine(waitForAttack(p));
        }
    }
    //apply the damage after 0.2 seconds
    IEnumerator waitForAttack(Collider2D p)
    {
        yield return new WaitForSeconds(0.2f);
        p.GetComponent<playermovement>().TakeDamage(AttDmg);
    }
    //take in a damage value and take away from currenthealth
    //if health reaches 0 call die function
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
    }

    //Play the death animation
    void Die()
    {
        animator.SetTrigger("IsDead");
    }
    //if a broken barrel has spawned, move to the most recent broken barrel and then wait
    public void Fix(){
        Flip();
        animator.SetBool("IsMoving", true);
        dmgwaiting = true;
        float dist = Vector2.Distance(transform.position, BBarrel.transform.position);
        if(dist>=1f){
            Vector3 direction = (BBarrel.transform.position - transform.position).normalized;
            rb.velocity = new Vector2(direction.x, direction.y) * speed;
        }
        if(dist<=1f){
            animator.SetBool("IsMoving", false);
            speed = 0;
            rb.velocity = Vector2.zero;
        }
    }
    //setter
    public  void setFix(){
            Fixing = true;
    }
    //setter
    public void setBarrel(GameObject x){
        BBarrel = x;
    }
    //reduce the amount of barrels left in the boss's memory
    public void countBarrel()
    {
        BarrelCount = BarrelCount -1;
    }
    //used to visualise the attack ranges in the Unity editor
    void OnDrawGizmosSelected()
    {
        if (AttackPointRight == null)
            return;
        Gizmos.DrawWireSphere(AttackPointRight.position, attackRange);
      
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
    //getter
    public bool getIsPlayerAlive()
    {
        return isPlayerAlive;
    }
    //used to reset the alive value if the game is replayed without closing
    public void retry()
    {
        Alive = true;
    }
    //getter
    public bool getAlive()
    {
        return Alive;
    }

}
