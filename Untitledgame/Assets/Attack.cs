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

    void Awake() {
        controls = new NewPlayeractions();
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
        animator.SetTrigger("Attack");
    }
}
