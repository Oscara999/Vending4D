using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLevelUp : StateMachineBehaviour
{
    Enemy boss;
    public float timeTexture;
    public float starTime;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Player.Instance.movimiento.CrossFireState(false);
        timeTexture = starTime; 
        boss = Enemy.Instance;
        //boss.ChageStateAnimation();
        boss.state = EnemyState.LEVELUP;
        boss.ChangeStates();
        boss.isMove = true;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.footsteps.CheckToPlayFootstepSound();

        if (!boss.IsActivate)
            return;

        //if (timeTexture < 0)
        //{
        //    boss.ChangeMaterial();
        //}
        //else
        //{
        //    timeTexture -= Time.fixedDeltaTime;
        //    Debug.Log(timeTexture);
        //}


        if (boss.isMove)
        {
            Debug.Log("esperando a caer al piso");
            Enemy.Instance.GoToPointStart();
        }
        else
        {
            Debug.Log("callo al piso");
            animator.SetBool("InGround", true);
        }
    }
}
