using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHuntState : StateMachineBehaviour
{
    GameObject pathingTo;
    GameObject player;
    EnemyDefault thisScript;
    List<GameObject> navs = new List<GameObject>();
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("ShouldCombat", false);
        player = GameObject.Find("player");
        navs = GameObject.Find("NavMesh").GetComponent<NavMeshGenerator>().navPoints;
        thisScript = animator.gameObject.GetComponent<EnemyDefault>();
        (GameObject target, int retType) = animator.gameObject.GetComponent<NavMeshCapableAgent>().AStar(animator.gameObject, player, navs);
        //Debug.Log(target);
        pathingTo = target;
        animator.SetFloat("AttackCooldown", 0);


        animator.gameObject.GetComponent<AudioSource>().clip = animator.gameObject.GetComponent<EnemyDefault>().run;
        animator.gameObject.GetComponent<AudioSource>().loop = true;
        animator.gameObject.GetComponent<AudioSource>().Play();

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector2.Distance(animator.gameObject.transform.position, player.transform.position) <= thisScript.Range)
        {
            animator.SetBool("ShouldCombat", true);
            //Debug.Log("Transitioning to Combat State");
        }

        if (animator.gameObject.GetComponent<EnemyDefault>().Die)
        {
            animator.SetBool("ShouldDie", true);
        }

        if(animator.gameObject.transform.position.x == pathingTo.transform.position.x) {
            int retType = 0;
            (pathingTo, retType) = animator.gameObject.GetComponent<NavMeshCapableAgent>().AStar(animator.gameObject, player, navs); 
        }
        Vector2 pathPt = new Vector2(pathingTo.transform.position.x, pathingTo.transform.position.y + animator.gameObject.GetComponent<SpriteRenderer>().bounds.extents.y);
        animator.gameObject.transform.position = Vector2.MoveTowards(animator.gameObject.transform.position, pathPt, thisScript.Speed);

        if (GameObject.Find("player").transform.position.x < animator.gameObject.transform.position.x)
        {
            animator.gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else { animator.gameObject.GetComponent<SpriteRenderer>().flipX = false; }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<AudioSource>().loop = false;
        animator.gameObject.GetComponent<AudioSource>().Stop();
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    /*override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }*/

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
