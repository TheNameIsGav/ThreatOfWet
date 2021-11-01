using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatState : StateMachineBehaviour
{
    //public GameObject enemy;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;
        animator.SetBool("ShouldHunt", false);
        animator.gameObject.GetComponent<EnemyDefault>().triggerDamage(true);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector2.Distance(animator.gameObject.transform.position, GameObject.Find("player").transform.position) > animator.gameObject.GetComponent<EnemyDefault>().Range)
        {
            animator.SetBool("ShouldHunt", true);
            Debug.Log("Transitioning to Hunt State");
        }

        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<EnemyDefault>().triggerDamage(false);
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
