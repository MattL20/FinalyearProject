using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawn : StateMachineBehaviour
{
    GameObject Enemy;
    private float _waitTime = 1f; // in seconds
    private float _waitCounter = 0f;
    private bool _waiting = false;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Enemy = GameObject.FindGameObjectWithTag("Boss");
        Enemy.GetComponent<Boss>().speed = 0;
        _waiting = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Enemy.GetComponent<Boss>().speed = 0;
        Debug.Log("Wait counter" + _waitCounter);
        if (_waiting)
        {
           // Debug.Log("Waiting");
            _waitCounter += Time.deltaTime;
            if (_waitCounter < _waitTime)
                return;
            _waiting = false;
            //Debug.Log("Destroy");
            Destroy(Enemy);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
