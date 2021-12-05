using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class EnemyFallAttack : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.gameObject.GetComponent<EnemyDefault>().InRange())
        {
            GameObject.Find("player").GetComponent<playerController>().ChangeHealth(-1f * animator.gameObject.GetComponent<EnemyDefault>().shouldAttack());
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(GameObject.Find("player").transform.position.x < animator.gameObject.transform.position.x)
        {
            animator.gameObject.GetComponent<SpriteRenderer>().flipX = true;
        } else { animator.gameObject.GetComponent<SpriteRenderer>().flipX = false; }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float atkspd = animator.gameObject.GetComponent<EnemyDefault>().attackSpeed;
        animator.SetFloat("AttackCooldown", Random.Range(4-atkspd, 6-atkspd));
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
