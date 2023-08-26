using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Singleton<Enemy>
{
   [Header("Enemy Stats")]
    public EnemyState state;
    public float speedGround;
    public float maxDistanceAttackGround;
    public float[] speedFly;
    public bool isInvulnerable;
    public bool isAttack;
    public bool isMove;
    [SerializeField] bool isActive;
    [SerializeField] GameObject[] powers;

    float rotation;
    float horizon;
    int destPoint;
    int index;
    [SerializeField]bool isInteracting;

    [Header("Enemy Settup")]
    [SerializeField] Animator animator;
    [SerializeField] Animator eyesAnimator;
    public Ray ray;
    public GameObject pointToScreen;
    public GameObject muzzle;
    public EnemyHealt healt;
    public EnemyFootsteps footsteps;
    public EnemySelected enemySelected;
    public ObjectPooling pooling;
    public Sound[] fx_Sound;

    [SerializeField] QTEEvent eventQTE;
    [SerializeField] GameObject startPoint;
    [SerializeField] GameObject startPointGround;
    [SerializeField] GameObject spawnPoint;
    [SerializeField] GameObject attackPoint;
    [SerializeField] GameObject body;
    [SerializeField] SkinnedMeshRenderer bodyRenderer;
    [SerializeField] Material[] phaseMaterials;

    [Header("Points Settup")]
    public List<int> orderWaitPoints1 = new List<int>();
    public List<int> orderWaitPoints2 = new List<int>();
    public List<int> orderWaitPoints3 = new List<int>();
    public List<GameObject> spawns = new List<GameObject>();
    
    [SerializeField] List<int> orderwaitPoints = new List<int>();
    [SerializeField] GameObject[] waitPoints;


    public bool IsActivate
    {
        set
        {
            isActive = value;
            if (isActive == false)
            {
                horizon = 0;
                rotation = 0;

                for (int i = 0; i < spawns.Count; i++)
                {
                    Destroy(spawns[i]);
                }

                spawns.Clear();
            }
        }
        get { return isActive; } }

    void Start()
    {
        StartCoroutine(footsteps.ChangeSound(false, 0.3f));
        footsteps.stepDistance = .35f;
        body.transform.LookAt(new Vector3(body.transform.rotation.x, Player.Instance.transform.transform.position.y, Player.Instance.transform.transform.position.z));
        SetOrderWaitPoints(ManagerGame.Instance.round);
        healt.lifes = 3; 
    }

    void FixedUpdate()
    {
        SetAnimation();
    }

    public void ChangeMaterial()
    {
        bodyRenderer.material = phaseMaterials[ManagerGame.Instance.round];
    }

    public void ChangeStates()
    {
        switch (state)
        {
            case EnemyState.PATROL:
                isInvulnerable = false;
                footsteps.stepDistance = .35f;
                break;

            case EnemyState.MOVE:
                isInvulnerable = true;
                StartCoroutine(footsteps.ChangeSound(false, .1f));
                footsteps.stepDistance = 0.35f; break;

            case EnemyState.GROUND:
                isInvulnerable = true;
                footsteps.stepDistance = 0.45f; 
                break;

            case EnemyState.DAMAGE:
                isInvulnerable = true;
                footsteps.stepDistance = .41f;
                break;

            case EnemyState.ATTACK:
                footsteps.stepDistance = .40f;
                break;

            case EnemyState.LEVELUP:
                StartCoroutine(footsteps.ChangeSound(true, 3.5f));
                isInvulnerable = true;
                footsteps.stepDistance = .35f;
                break;

            case EnemyState.QTE:
                footsteps.stepDistance = 20f;
                break;

            case EnemyState.DEAD:
                break;
            default:
                break;
        }
    }

    public void SetOrderWaitPoints(int round)
    {
        destPoint = 0;
        index = 0;

        switch (round)
        {
            case 0 : 
                orderwaitPoints = orderWaitPoints1;
                break;

            case 1 :
                orderwaitPoints = orderWaitPoints2;
                break;

            case 2:
                orderwaitPoints = orderWaitPoints3;
                break;
        }

        healt.RestoreLife();

    }

    public void AttackFly()
    {
        GameObject test = Instantiate(powers[ManagerGame.Instance.round], spawnPoint.transform.position, body.transform.rotation);
        spawns.Add(test);
        test.GetComponent<EnemyBullet>().SetTarget(Player.Instance.transform.position);
        test.GetComponent<EnemyBullet>().SetLookTarget(ray.origin);
        SoundManager.Instance.PlayNewSound(fx_Sound[0].name);
    }

    public void AttackGround()
    {
        ManagerGame.Instance.QTEManager.StartEvent(eventQTE);
        SoundManager.Instance.PlayNewSound(SoundManager.Instance.songs[0].name);
    }

    public void ChageStateAnimation()
    {
       isInteracting = !isInteracting;
    }

    public IEnumerator LevelUp()
    {
        yield return new WaitUntil(() => spawns.Count == 0);
        animator.SetTrigger("LevelUp");
    }

    public void SlowMode(bool state)
    {
        if (state)
        {
            eyesAnimator.SetBool("Slow", true);
        }
        else
        {
            eyesAnimator.SetBool("Slow", false);
        }
    }

    public void DamageQTE(bool state)
    {
        animator.SetBool("QTEFail", state);

        if (state)
        {
            SoundManager.Instance.EndSound(SoundManager.Instance.songs[0].name);
        }
    }

    public void Die()
    {
        animator.SetBool("Dead", true);
    }

    public void GetDamage(int value)
    {
        if (healt.health > 0)
        {
            animator.SetTrigger("GetDamage");
            //StatesManager.Instance.ui.lifeEnemyObject.GetComponent<Animator>().SetTrigger("Damage");
            healt.TakeDamage(value);
            Debug.Log("Damage");
        }
    }

    public void SetAnimation()
    {
        animator.SetFloat("Velocity", horizon, 0.1f, Time.deltaTime);
        animator.SetFloat("Rotation", rotation, 0.1f, Time.deltaTime);
    }

    public void MoveFlyPatrol()
    {
        if (isInteracting)
        {
            rotation = 0;
            horizon = 0;
            return;
        }

        GotoNextPointFly();
        OrientationFly();
    }

    void OrientationFly()
    {
        if (transform.position.x > waitPoints[destPoint].transform.position.x)
        {
            if (rotation > -.7f)
            {
                rotation -= Time.fixedDeltaTime;
            }
        }
        else if (transform.position.x < waitPoints[destPoint].transform.position.x)
        {
            if (rotation < .7f)
            {
                rotation += Time.fixedDeltaTime;
            }
        }

        if (transform.position.y > waitPoints[destPoint].transform.position.y)
        {
            if (horizon > -0.39f)
            {
                horizon -= Time.fixedDeltaTime;
            }
        }
        else if (transform.position.y < waitPoints[destPoint].transform.position.y)
        {
            if (horizon < 0.39f)
            {
                horizon += Time.fixedDeltaTime;
            }
        }
    }

    void GotoNextPointFly()
    {
        // Returns if no points have been set up
        if (orderwaitPoints.Count == 0 || isInteracting)
            return;

        transform.position = (Vector3.MoveTowards(transform.position, waitPoints[destPoint].transform.position, speedFly[ManagerGame.Instance.round] * Time.deltaTime));
        body.transform.LookAt(new Vector3(body.transform.rotation.x, Player.Instance.transform.transform.position.y, Player.Instance.transform.transform.position.z));

        if (Vector3.Distance(transform.position, waitPoints[destPoint].transform.position) < 0.1f && !isAttack)
        { 
            NextPointFly();

            if (destPoint != index)
            {
                animator.SetBool("Attack", true);
                isAttack = true;
            }
        }
    }

    void NextPointFly()
    {
        index = (index + 1) % orderwaitPoints.Count;

        if (index != 0)
        {
            destPoint = orderwaitPoints[index];
        }
        else
        {  
            StartCoroutine(LevelUp());
            Debug.Log("Aqujiiisadd");
            ChageStateAnimation();
         }
    }

    public void MoveGround()
    {
        if (isInteracting)
        {
            horizon = 0;
            return;
        }

        Debug.Log("Moviendome");
        
        GotoNextPointGround();

        if (Vector3.Distance(transform.position, attackPoint.transform.position) > maxDistanceAttackGround)
        {
            if (horizon < 1f)
            {
                horizon += Time.fixedDeltaTime;
            }
        }
        else
        {
            if (horizon >= 0)
            {
                horizon -= Time.deltaTime;
            }
        }
    }

    void GotoNextPointGround()
    {
        // Returns if no points have been set up
        if (attackPoint == null || isInteracting)
            return;

        transform.position = (Vector3.MoveTowards(transform.position, attackPoint.transform.position, speedGround * Time.deltaTime));
        body.transform.LookAt(new Vector3(body.transform.rotation.x, Player.Instance.transform.transform.position.y, Player.Instance.transform.transform.position.z));

        if (Vector3.Distance(transform.position, attackPoint.transform.position) < 0.1f && !isAttack)
        {
            Debug.Log("sss");
            isAttack = true;
            animator.SetBool("Attack", true);
        }
    }

    public void GoToPointStart()
    {
        if (startPoint == null || startPointGround == null)
            return;

        transform.position = (Vector3.MoveTowards(transform.position, startPointGround.transform.position, speedGround * Time.deltaTime));
        body.transform.LookAt(new Vector3(body.transform.rotation.x, Player.Instance.transform.transform.position.y, Player.Instance.transform.transform.position.z));

        if (Vector3.Distance(transform.position, startPointGround.transform.position) < 0.1f)
        {
            Debug.Log("llego al suelo");
            isMove = false;
        }
    }

    public void ReturnToPointStart()
    {
        if (startPoint == null || startPointGround == null)
            return;

        transform.position = (Vector3.MoveTowards(transform.position, startPoint.transform.position, speedGround * Time.deltaTime));
        body.transform.LookAt(new Vector3(body.transform.rotation.x, Player.Instance.transform.transform.position.y, Player.Instance.transform.transform.position.z));

        if (Vector3.Distance(transform.position, startPoint.transform.position) < 0.1f)
        {
            StatesManager.Instance.uiController.CrossFireState(true);
            isMove = false;
            
            if (ManagerGame.Instance.round == 3)     
            {
                ManagerGame.Instance.FinishGame();
            }
        }
    }

    public void InGround()
    {
        if (startPoint == null || isInteracting)
            return;

        Vector3 groundPosition = new Vector3(transform.position.x, startPoint.transform.position.y, transform.position.z);
        transform.position = (Vector3.MoveTowards(transform.position, groundPosition , speedFly[ManagerGame.Instance.round] * Time.deltaTime));
 
        if (Vector3.Distance(transform.position, groundPosition) < 0.1f)
        {
            isMove = false;
        }
    }
}
