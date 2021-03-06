using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnState : StateMachineBehaviour
{
    //public GameObject enemy;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Should be commented out once the spawn tiles are going to handle calling the spawn function
        //animator.gameObject.GetComponent<EnemyDefault>().Spawn(Vector2.zero, 4);
        (GameObject target, int retType) = animator.gameObject.GetComponent<NavMeshCapableAgent>().AStar(animator.gameObject, 
                                                                                                         GameObject.Find("player"), 
                                                                                                         GameObject.Find("NavMesh").GetComponent<NavMeshGenerator>().navPoints);

        Vector2 pathPt = new Vector2(target.transform.position.x, target.transform.position.y + (animator.gameObject.GetComponent<SpriteRenderer>().bounds.extents.y));
        if ( retType == 1 ) animator.gameObject.transform.position = pathPt;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
