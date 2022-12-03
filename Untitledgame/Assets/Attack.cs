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
    public Rigidbody2D rb;

    playermovement movescript; 

    void Awake() {
        controls = new NewPlayeractions();
        movescript = rb.GetComponent<playermovement>();
    }
    
    void OnEnable() {
  
        att = controls.Player.Attack;
       
       att.Enable(); 
       att.performed += Attacks;
        


    }
    void OnDisable() {
       att.Disable();
        
    }
    void Attacks(InputAction.CallbackContext context){
        movescript.canmove = false;
        animator.SetTrigger("Attack");
        

    }
}
