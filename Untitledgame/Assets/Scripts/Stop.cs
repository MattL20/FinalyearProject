using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//attached to the attacking animation for the player
//slows the player movement when attacking
public class Stop : StateMachineBehaviour
{
    GameObject player;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<playermovement>().MovementSpeed = 1;
    }
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player.GetComponent<playermovement>().MovementSpeed = 6;
    }
}
