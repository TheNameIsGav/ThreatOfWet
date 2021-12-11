using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatState : StateMachineBehaviour
{
    GameObject player;
    EnemyDefault thisScript;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;
        animator.SetBool("ShouldHunt", false);
        player = GameObject.Find("player");
        thisScript = animator.gameObject.GetComponent<EnemyDefault>();
        //Debug.Log("Set attack cooldown to " + animator.GetFloat("AttackCooldown") + " seconds (ish)");
        animator.SetFloat("AttackCooldown", animator.GetFloat("AttackCooldown") * 60);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector2.Distance(animator.gameObject.transform.position, player.transform.position) > thisScript.Range)
        {
            animator.SetBool("ShouldHunt", true);
            //Debug.Log("Transitioning to Hunt State");
        } else
        {
            if (animator.GetFloat("AttackCooldown") == 0) { animator.Play("RaiseAttack", layerIndex, 1); }
            else { animator.SetFloat("AttackCooldown", animator.GetFloat("AttackCooldown") - 1); }
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


