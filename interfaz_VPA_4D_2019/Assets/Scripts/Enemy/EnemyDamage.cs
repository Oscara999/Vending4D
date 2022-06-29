using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class EnemyDamage : StateMachineBehaviour
{
    Enemy boss;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = Enemy.Instance;
        boss.state = EnemyState.DAMAGE;
        boss.ChangeStates();
        boss.ChageStateAnimation();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.footsteps.CheckToPlayFootstepSound();
        if (!boss.IsActivate)
            return;

        boss.MoveFlyPatrol();
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.ChageStateAnimation();
        //boss.isInvulnerable = false;
    }
}
