using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    // Start is called before the first frame update
    public NewPlayeractions controls;
    public Animator animator;
    private InputAction att;
    public Transform AttackPointDown;
    public Transform AttackPointUp;
    public Transform AttackPointLeft;
    public Transform AttackPointRight;
    public GameObject player;

    public float attackRange = 0.5f;
    public LayerMask enemyLayer;
    private float LastX = 0;
    private float LastY = 0;
    private playermovement pm;
    private Collider2D[] hitEnemies;
    // public Transform player;
    private int AttackDmg = 20;

    void Awake() {
        controls = new NewPlayeractions();
    }
    
    void OnEnable() {

        player = GameObject.FindGameObjectWithTag("Player");
        pm = player.GetComponent<playermovement>();
       att = controls.Player.Attack;
       att.Enable(); 
       att.performed += Attacks;
        

       
    }
    void OnDisable() {
       att.Disable();
        //Debug.Log(att);
    }
    void Attacks(InputAction.CallbackContext context){
        animator.SetTrigger("Attack");
        LastX = pm.LastX;
        LastY = pm.LastY;
        //Debug.Log(LastX);
        //Debug.Log(LastY);
        
        if (LastX == 1 && LastY == 0)
        {
           hitEnemies = Physics2D.OverlapCircleAll(AttackPointRight.position, attackRange, enemyLayer);
        }else if (LastX == -1 && LastY == 0)
        {
            hitEnemies = Physics2D.OverlapCircleAll(AttackPointLeft.position, attackRange, enemyLayer);
        }else if (LastX == 0 && LastY == 1)
        {
          hitEnemies = Physics2D.OverlapCircleAll(AttackPointUp.position, attackRange, enemyLayer);
        }else if (LastX == 0 && LastY == -1)
        {
           hitEnemies = Physics2D.OverlapCircleAll(AttackPointDown.position, attackRange, enemyLayer);
        }
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Boss>().TakeDamage(AttackDmg);
        }
    }
    //void OnDrawGizmosSelected()
    //{
    //    if (AttackPointRight == null)
    //        return;
    //    if (AttackPointLeft == null)
    //        return;
    //    if (AttackPointUp == null)
    //        return;
    //    if (AttackPointDown == null)
    //        return;
    //    Gizmos.DrawWireSphere(AttackPointRight.position, attackRange);
    //    Gizmos.DrawWireSphere(AttackPointLeft.position, attackRange);
    //    Gizmos.DrawWireSphere(AttackPointUp.position, attackRange);
    //    Gizmos.DrawWireSphere(AttackPointDown.position, attackRange);
    //}
}
