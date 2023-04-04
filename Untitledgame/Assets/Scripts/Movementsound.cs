using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Attached to the player movement animations, plays the walking sound effect during the walking animation
public class Movementsound : StateMachineBehaviour
{
    public AudioSource walking;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        walking.Play();
    }
    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        walking.Stop();
    }
}
