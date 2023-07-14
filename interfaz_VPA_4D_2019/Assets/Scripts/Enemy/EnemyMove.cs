using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : StateMachineBehaviour
{
    Enemy boss;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        StatesManager.Instance.leapMotionMovementController.CrossFireState(false);
        boss = Enemy.Instance;
        boss.ChageStateAnimation();
        boss.state = EnemyState.MOVE;
        boss.ChangeStates();
        boss.isMove = true;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //boss.footsteps.CheckToPlayFootstepSound();

        if (!boss.IsActivate)
            return;

        if (boss.isMove)
        {
            Enemy.Instance.ReturnToPointStart();
        }
        else
        {
           //ManagerSound.Instance.PlayNewSound("DragonDamage");
            animator.SetBool("InGround", false);
        }
    }
}
