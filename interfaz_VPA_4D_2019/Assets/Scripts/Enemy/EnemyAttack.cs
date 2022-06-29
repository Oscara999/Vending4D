using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : StateMachineBehaviour
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
        boss.state = EnemyState.ATTACK;
        boss.ChangeStates();
        attack = true;
        boss.muzzle.SetActive(true);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!boss.IsActivate || boss.isInvulnerable)
        {
            animator.SetBool("Attack", false);
            return;
        }

        boss.ray = Camera.main.ScreenPointToRay(boss.pointToScreen.transform.position);
        Debug.DrawRay(boss.ray.origin, boss.transform.position, Color.yellow);

        if (time <= 0 && attack)
        {
            attack = false;
            boss.AttackFly();
            animator.SetBool("Attack", false);
        }
        else
        {
            time -= Time.deltaTime;
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.muzzle.SetActive(false);
        boss.isAttack = false;
        boss.ChageStateAnimation();
    }
}
