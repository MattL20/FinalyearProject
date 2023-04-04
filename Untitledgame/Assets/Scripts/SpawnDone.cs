using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//on the boss spawn animation, stops movement during the animation and then allows it when the boss is done spawning
public class SpawnDone : StateMachineBehaviour
{
    GameObject Boss;
    private float _waitTime = 1f; 
    private float _waitCounter = 0f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Boss = GameObject.FindGameObjectWithTag("Boss");
    }
    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!animator.GetBool("CanMove"))
        {
            _waitCounter += Time.deltaTime;
            if (_waitCounter < _waitTime)
                return;
            animator.SetBool("CanMove", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
            if (!Boss.GetComponent<Boss>().canMove)
        {
            _waitCounter += Time.deltaTime;
            if (_waitCounter < _waitTime)
                return;
            Boss.GetComponent<Boss>().canMove = true;
        }
    }
}
