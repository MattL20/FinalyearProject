using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    public NewPlayeractions controls;
    public Animator animator;
    private InputAction att;
    //Reference to the attack points on the player object
    public Transform AttackPointDown;
    public Transform AttackPointUp;
    public Transform AttackPointLeft;
    public Transform AttackPointRight;
    //Reference to the player object
    public GameObject player;
    //Range of attacks
    public float attackRange = 0.5f;
    //Layer that the enemy or the items are in 
    public LayerMask enemyLayer;
    public LayerMask itemLayer;
    //Last direction faced
    private float LastX = 0;
    private float LastY = 0;
    //Reference to the player movement scripts
    private playermovement pm;
    //Arrays to hold reference to enemies hit and the items hit
    private Collider2D[] hitEnemies;
    private Collider2D[] hitItems;
    //Damage to do the boss
    private int AttackDmg = 2;
    //Damage to do the barrels
    private int BarrelDmg = 5;
    // awake is called before the first frame update and before start
    void Awake() {
        controls = new NewPlayeractions();
    }
    //when enabled get the controls from the input actions
    void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pm = player.GetComponent<playermovement>();
        att = controls.Player.Attack;
        att.Enable();
        att.performed += Attacks;
    }
    void OnDisable() {
       att.Disable();
    }
    //when attack is called by input play attack animation in the correct direction and collect all enemies and items hit and apply damage to them
    void Attacks(InputAction.CallbackContext context){
        animator.SetTrigger("Attack");
        LastX = pm.LastX;
        LastY = pm.LastY;
        if (LastX == 1 && LastY == 0)
        {
           hitEnemies = Physics2D.OverlapCircleAll(AttackPointRight.position, attackRange, enemyLayer);
           hitItems = Physics2D.OverlapCircleAll(AttackPointRight.position, attackRange, itemLayer);
        }else if (LastX == -1 && LastY == 0)
        {
            hitEnemies = Physics2D.OverlapCircleAll(AttackPointLeft.position, attackRange, enemyLayer);
            hitItems = Physics2D.OverlapCircleAll(AttackPointLeft.position, attackRange, itemLayer);
        }else if (LastX == 0 && LastY == 1)
        {
          hitEnemies = Physics2D.OverlapCircleAll(AttackPointUp.position, attackRange, enemyLayer);
          hitItems = Physics2D.OverlapCircleAll(AttackPointUp.position, attackRange, itemLayer);
        }else if (LastX == 0 && LastY == -1)
        {
           hitEnemies = Physics2D.OverlapCircleAll(AttackPointDown.position, attackRange, enemyLayer);
           hitItems = Physics2D.OverlapCircleAll(AttackPointDown.position, attackRange, itemLayer);
        }
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Boss>().TakeDamage(AttackDmg);
        }
        foreach(Collider2D items in hitItems)
        {
            if(items.tag=="Barrel"){
            items.GetComponent<BarrelScript>().TakeDamage(BarrelDmg);
            }
            if(items.tag=="HalfBrokenBarrel"){
            items.GetComponent<HalfBrokenBarrel>().TakeDamage(BarrelDmg);  
            }
        }
    }
    //Used to visualise the attack ranges
    void OnDrawGizmosSelected()
    {
        if (AttackPointRight == null)
            return;
        if (AttackPointLeft == null)
            return;
        if (AttackPointUp == null)
            return;
        if (AttackPointDown == null)
            return;
        Gizmos.DrawWireSphere(AttackPointRight.position, attackRange);
        Gizmos.DrawWireSphere(AttackPointLeft.position, attackRange);
        Gizmos.DrawWireSphere(AttackPointUp.position, attackRange);
        Gizmos.DrawWireSphere(AttackPointDown.position, attackRange);
    }
}
