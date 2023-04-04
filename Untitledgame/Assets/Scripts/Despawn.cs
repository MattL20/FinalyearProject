using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//A script within the Boss death animation
public class Despawn : StateMachineBehaviour
{
    GameObject Enemy;
    //timer variables
    private float _waitTime = 1f; // in seconds
    private float _waitCounter = 0f;
    private bool _waiting = false;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Enemy = GameObject.FindGameObjectWithTag("Boss");//find reference to the boss game object
        Enemy.GetComponent<Boss>().speed = 0;//stop movement
        _waiting = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        Enemy.GetComponent<Boss>().speed = 0;
        //wait 1 second after animation then destroy the boss game object
        if (_waiting)
        {
            _waitCounter += Time.deltaTime;
            if (_waitCounter < _waitTime)
                return;
            _waiting = false;
            Destroy(Enemy);
        }
    }
}
