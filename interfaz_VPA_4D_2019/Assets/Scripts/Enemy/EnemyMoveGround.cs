using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveGround : StateMachineBehaviour
{
    Enemy boss;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = Enemy.Instance;
        boss.state = EnemyState.GROUND;
        boss.ChageStateAnimation();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.footsteps.CheckToPlayFootstepSound();

        if (!boss.IsActivate)
            return;

        if (animator.GetBool("InGround"))
        {
            boss.MoveGround();
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.ChageStateAnimation();
        StatesManager.Instance.uiController.CrossFireState(true);
    }
}
