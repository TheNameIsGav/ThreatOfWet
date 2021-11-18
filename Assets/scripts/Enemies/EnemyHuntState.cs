using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHuntState : StateMachineBehaviour
{
    GameObject pathingTo;
    GameObject player;
    List<GameObject> navs = new List<GameObject>();
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("ShouldCombat", false);
        player = GameObject.Find("player");
        navs = GameObject.Find("NavMesh").GetComponent<NavMeshGenerator>().navPoints;
        (GameObject target, int retType) = animator.gameObject.GetComponent<NavMeshCapableAgent>().AStar(animator.gameObject, GameObject.Find("player"), GameObject.Find("NavMesh").GetComponent<NavMeshGenerator>().navPoints);
        Debug.Log(target);
        pathingTo = target;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector2.Distance(animator.gameObject.transform.position, GameObject.Find("player").transform.position) <= /*animator.gameObject.GetComponent<EnemyDefault>().Range*/ 2f)
        {
            animator.SetBool("ShouldCombat", true);
            //Debug.Log("Transitioning to Combat State");
        }

        if (animator.gameObject.GetComponent<EnemyDefault>().Die)
        {
            animator.SetBool("ShouldDie", true);
        }

        
        if(animator.gameObject.transform.position == pathingTo.transform.position) {
            int retType = 0;
            (pathingTo, retType) = animator.gameObject.GetComponent<NavMeshCapableAgent>().AStar(animator.gameObject, GameObject.Find("player"), GameObject.Find("NavMesh").GetComponent<NavMeshGenerator>().navPoints); 
        }

        animator.gameObject.transform.position = Vector2.MoveTowards(animator.gameObject.transform.position, pathingTo.transform.position, .01f);

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
        
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    /*override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //TODO Change Navmesh accessors to be the game manager reference
        (Vector2 move, bool jump) = GameObject.Find("NavMesh").GetComponent<NavMeshGenerator>().FindNextPointAlongPath(animator.gameObject.transform.position, /*animator.gameObject.GetComponent<EnemyDefault>().PathR*/ 20f);
        if (jump)
        {
            //Debug.Log("Jumping to " + move);
            animator.gameObject.transform.position = new Vector3(move.x, move.y + 1);
        }
        else
        {
            //Debug.Log("walking towards " + move);
            //Debug.Log(animator.gameObject.transform.position);
            animator.gameObject.transform.position = Vector3.MoveTowards(animator.gameObject.transform.position, new Vector3(move.x, animator.gameObject.transform.position.y), .01f);
            //Debug.Log(animator.gameObject.transform.position);
        }
    }*/

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
