using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatState : StateMachineBehaviour
{
    //public GameObject enemy;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;
        animator.SetBool("ShouldHunt", false);
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector2.Distance(animator.gameObject.transform.position, GameObject.Find("player").transform.position) > 20)
        {
            animator.SetBool("ShouldHunt", true);
            //Debug.Log("Transitioning to Hunt State");
        }

        if (animator.gameObject.GetComponent<EnemyDefault>().Die)
        {
            animator.SetBool("ShouldDie", true);
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
