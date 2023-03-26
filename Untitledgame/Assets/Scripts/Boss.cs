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
    private int AttDmg = 25;


    private float _waitTime = 1f; 
    private float _waitCounter = 0f;
    private bool _waiting = false;

    private float _waitTime2 = 1f; 
    private float _waitCounter2 = 0f;
    private bool _waiting2 = false;

    private float dmgwaitTime = 5f; 
    private float dmgwaitCounter = 0f;
    private static bool dmgwaiting = false;

    public AudioSource punch;

    private bool hasAttacked = false;
    private bool InAgro = false;

    public int maxHealth = 100;
    private static int currentHealth;
   

    public Transform AttackPointRight;
    public float attackRange = 1.2f;
    public LayerMask playerLayer;
    private Collider2D[] hitPlayer;

    private static GameObject BBarrel;
    private static int BarrelCount = 8;

    private static bool Fixing = false;


    public static bool isPlayerAlive;

    
    Rigidbody2D rb;
    Transform Target;
    Vector2 moveDirection;
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        
        currentHealth = maxHealth;
    }
    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        BarrelCount = 8;
        Alive = true;
    }
    private void Update() {
        
        float dist = Vector2.Distance(transform.position,player.position);
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
            if (canMove)
            {
                Move();    
            }
            isPlayerAlive = player.GetComponent<playermovement>().getAlive();
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
    }
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
        InAgro = true;
        animator.SetBool("IsMoving", true);
        canMove = false;
        Vector3 direction = (player.transform.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x, direction.y) * speed;
        }
    
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
    IEnumerator waitForAttack(Collider2D p)
    {
        yield return new WaitForSeconds(0.2f);
        p.GetComponent<playermovement>().TakeDamage(AttDmg);
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
    public  void setFix(){
            Fixing = true;
    }
    public void setBarrel(GameObject x){
        BBarrel = x;
    }
    public void countBarrel()
    {
        BarrelCount = BarrelCount -1;
    }
    void OnDrawGizmosSelected()
    {
        if (AttackPointRight == null)
            return;
        Gizmos.DrawWireSphere(AttackPointRight.position, attackRange);
      
    }
    public int getHealth()
    {
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
    public void retry()
    {
        Alive = true;
    }
    public bool getAlive()
    {
        return Alive;
    }

}
