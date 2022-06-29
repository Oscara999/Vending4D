using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackGround : StateMachineBehaviour
{
    Enemy boss;
    public float startTime;
    public float time;
    bool attack;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        time = startTime;
        boss = Enemy.Instance;
        boss.ChageStateAnimation();
        boss.state = EnemyState.QTE;
        boss.ChangeStates();
        attack = true;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!boss.IsActivate)
            return;

        if (time <= 0 && attack)
        {
            attack = false;
            boss.AttackGround();
            animator.ResetTrigger("Attack");
        }
        else
        {
            time -= Time.deltaTime;
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.isAttack = false;
        boss.ChageStateAnimation();
    }
}
