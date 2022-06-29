using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolFly : StateMachineBehaviour
{
    Enemy boss;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = Enemy.Instance;
        boss.state = EnemyState.PATROL;
        boss.ChangeStates();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.footsteps.CheckToPlayFootstepSound();

        if (!boss.IsActivate)
            return;
        boss.MoveFlyPatrol();
    }
    
    //// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //}
}
