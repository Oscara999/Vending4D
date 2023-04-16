using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Singleton<Enemy>
{
    public Sound[] fx_Sound;
    public List<int> orderWaitPoints1 = new List<int>();
    public List<int> orderWaitPoints2 = new List<int>();
    public List<int> orderWaitPoints3 = new List<int>();
    public EnemyState state;
    public EnemyHealt healt;
    public EnemyFootsteps footsteps;
    public EnemySelected enemySelected;
    public ObjectPooling pooling;
    public GameObject pointToScreen;
    public GameObject muzzle;
    public GameObject lifeObject;
    public Ray ray;
    public float speedGround;
    public float[] speedFly;
    public bool isAttack;
    public bool isMove;
    public bool isInvulnerable = false;
    public List<GameObject> spawns = new List<GameObject>();

    List<int> orderwaitPoints = new List<int>();
    public int index;
    int destPoint;
    float rotation;
    float horizon;
    bool isInteracting;

    [SerializeField] bool isActive;
    [SerializeField] Animator animator;
    [SerializeField] QTEEvent eventQTE;
    [SerializeField] Material[] phaseMaterials;
    [SerializeField] GameObject[] waitPoints;
    [SerializeField] GameObject[] powers;
    [SerializeField] GameObject startPoint;
    [SerializeField] GameObject startPointGround;
    [SerializeField] GameObject spawnPoint;
    [SerializeField] GameObject attackPoint;
    [SerializeField] GameObject body;
    [SerializeField] SkinnedMeshRenderer bodyRenderer;

    public bool IsActivate
    {
        set
        {
            isActive = value;
            if (isActive == false)
            {
                horizon = 0;
                rotation = 0;

                for (int i = 0; i < Enemy.Instance.spawns.Count; i++)
                {
                    Destroy(Enemy.Instance.spawns[i]);
                }

                Enemy.Instance.spawns.Clear();
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
        if (state == EnemyState.PATROL)
        {
            isInvulnerable = false;
            footsteps.stepDistance = .35f;
        }

        else if (state == EnemyState.LEVELUP)
        {
            StartCoroutine(footsteps.ChangeSound(true, 3.5f));
            isInvulnerable = true;
            footsteps.stepDistance = .35f;
        }

        else if (state == EnemyState.ATTACK)
        {
            footsteps.stepDistance = .40f;
        }

        else if (state == EnemyState.QTE)
        {
            footsteps.stepDistance = 20f;
        }

        else if (state == EnemyState.DAMAGE)
        {
            isInvulnerable = true;
            footsteps.stepDistance = .41f;
        }

        else if (state == EnemyState.GROUND)
        {
            isInvulnerable = true;
            footsteps.stepDistance = 0.45f;
        }

        else if (state == EnemyState.MOVE)
        {
            isInvulnerable = true;
            StartCoroutine(footsteps.ChangeSound(false, .1f));
            footsteps.stepDistance = 0.35f;
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
                healt.RestoreLife();
                break;

            case 1 :
                orderwaitPoints = orderWaitPoints2;
                healt.RestoreLife();
                break;

            case 2:
                orderwaitPoints = orderWaitPoints3;
                healt.RestoreLife();
                break;
        }
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

    public void LevelUp()
    {
        animator.SetTrigger("LevelUp");
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
            lifeObject.GetComponent<Animator>().SetTrigger("Damage");
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
            animator.SetBool("Attack", true);
            isAttack = true;
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
            Enemy.Instance.LevelUp();
        }
    }

    public void MoveGround()
    {
        if (isInteracting)
        {
            horizon = 0;
            return;
        }

        GotoNextPointGround();

        if (Vector3.Distance(transform.position, attackPoint.transform.position) > 0.4f)
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
            StatesManager.Instance.ui.uIController.CrossFireState(true);
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
