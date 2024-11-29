using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    PATROL,CHASE,ATTACK
}

public class EnemyController : MonoBehaviour
{
    private EnemyAnimations enemyAnim;
    private NavMeshAgent navAgent;
    private EnemyState enemyState;
    private EnemyAudio enemyAudio;

    private Transform target;
    public GameObject attackPoint;

    public float walkSpeed = 0.5f;
    public float runSpeed = 4f;

    public float chaseDistance = 7f;
    public float attackDistance = 1.8f;
    public float chaseAftrAttackDis = 2f;

    public float patrolRadiusMin = 20f, patrolRadiusMax = 60f;
    public float patrolThisTime = 15f;
    public float waitBeforeAttack = 2f;

    private float currentChaseDistance;
    private float patrolTimer, attackTimer;

    public EnemyState Enemy_State
    {
        get; set;
    }

    private void Awake()
    {
        enemyAnim = GetComponent<EnemyAnimations>();
        navAgent = GetComponent<NavMeshAgent>();

        target = GameObject.FindWithTag(Tag.PLAYER_TAG).transform;

        enemyAudio = GetComponentInChildren<EnemyAudio>();
    }

    private void Start()
    {
        enemyState = EnemyState.PATROL;

        patrolTimer = patrolThisTime;
        attackTimer = waitBeforeAttack;
        currentChaseDistance = chaseDistance;
    }

    void Update()
    {
        CheckEnemyState();
    }

    void CheckEnemyState()
    {
        if (enemyState == EnemyState.PATROL)
        {
            Patrol();
        }
        if (enemyState == EnemyState.ATTACK)
        {
            Attack();
        }
        if (enemyState == EnemyState.CHASE)
        {
            Chase();
        }
    }

    // PATROL ENEMY
    void Patrol()
    {
        navAgent.isStopped = false;
        navAgent.speed = walkSpeed;

        patrolTimer += Time.deltaTime;
        if (patrolTimer > patrolThisTime)
        {
            SetNewRandomDestination();
            patrolTimer = 0f;
        }
        EnemyPatrolAnimation();

        // Check Distance
        if (Vector3.Distance(transform.position, target.position) <= chaseDistance)
        {
            enemyAnim.Walk(false);
            enemyState = EnemyState.CHASE;

            enemyAudio.PlayScreamSound();
        }
    }

    void SetNewRandomDestination()
    {
        float randomRadius = Random.Range(patrolRadiusMin, patrolRadiusMax);
        Vector3 randomDir = Random.insideUnitSphere * randomRadius;
        randomDir += transform.position;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDir, out navHit, randomRadius, -1);

        navAgent.SetDestination(navHit.position);
    }

    void EnemyPatrolAnimation()
    {
        if (navAgent.velocity.sqrMagnitude > 0)
        {
            enemyAnim.Walk(true);
        }
        else
        {
            enemyAnim.Walk(false);
        }
    }

    // ENEMY CHASE
    void Chase()
    {
        navAgent.isStopped = false;
        navAgent.speed = runSpeed;
        navAgent.SetDestination(target.position);

        EnemyChaseAnimation();

        // Check Distance
        if (Vector3.Distance(transform.position, target.position) <= attackDistance)
        {
            enemyAnim.Run(false);
            enemyAnim.Walk(false);
            enemyState = EnemyState.ATTACK;

            if(chaseDistance != currentChaseDistance)
            {
                chaseDistance = currentChaseDistance;
            }
        }
        else if(Vector3.Distance(transform.position,target.position)> chaseDistance) 
        {
            enemyAnim.Run(false);
            enemyState = EnemyState.PATROL;
            patrolTimer = patrolThisTime;

            if (chaseDistance != currentChaseDistance)
            {
                chaseDistance = currentChaseDistance;
            }
        }
    }

    void EnemyChaseAnimation()
    {
        if (navAgent.velocity.sqrMagnitude > 0)
        {
            enemyAnim.Run(true);
        }
        else
        {
            enemyAnim.Run(false);
        }
    }

    // ENEMY ATTACK
    void Attack()
    {
        navAgent.velocity = Vector3.zero;
        navAgent.isStopped = true;

        attackTimer += Time.deltaTime;

        if (attackTimer > waitBeforeAttack)
        {
            enemyAnim.Attack();
            attackTimer = 0;

            enemyAudio.PlayAttackSound();
        }

        // Check Distance
        if(Vector3.Distance(transform.position,target.position)> attackDistance + chaseAftrAttackDis)
        {
            enemyState =EnemyState.CHASE;
        }
    }

    void OnAttackPoint()
    {
        attackPoint.SetActive(true);
    }

    void OffAttackPoint()
    {
        if (attackPoint.activeInHierarchy)
        {
            attackPoint.SetActive(false);
        }
    }

}
