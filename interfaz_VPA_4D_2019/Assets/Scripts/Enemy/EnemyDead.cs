using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class EnemyDead : StateMachineBehaviour
{
    Enemy boss;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = Enemy.Instance;
        boss.state = EnemyState.DEAD;
        boss.ChangeStates();
        boss.isMove = true;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!boss.IsActivate)
            return;

        Debug.Log("salio");

        if (boss.isMove)
        {
            boss.InGround();
        }
        else
        {
            animator.SetBool("InGround", true);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
    {
        animator.SetBool("IsDead", false);
        boss.ChageStateAnimation();
        ManagerGame.Instance.FinishGame();

    }
}
