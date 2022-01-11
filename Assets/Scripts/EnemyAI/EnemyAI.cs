using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


public enum AIState
{
    Idle,
    MoveToPatrol,
    MoveToPlayer,
    Attack,
    Stare,
    Investigate
}

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    [Header("AI State")]
    [SerializeField]
    protected AIState currentState;

    [SerializeField]
    protected AIState previousState;

    [Header("AI Decision TImes")]
    [SerializeField]
    float lastThinkTime;

    [SerializeField]
    float thinkRate = 0.5f;

    [Header("AI Detection")]
    [SerializeField]
    protected float detectionRange = 15f;

    [SerializeField]
    protected Vector3 playerPos;

    [SerializeField]
    Vector3 playerOffset = new Vector3(0, 1, 0);

    [SerializeField]
    protected LayerMask LOSLayer;

    [SerializeField]
    protected Vector2 moveToPlayerWaitTime = new Vector2(1f, 2f);

    [SerializeField]
    protected float moveToPlayerWaitTime_Now = 0;

    [Header("AI Patrol")]

    [SerializeField]
    protected Vector3 patrolPos;

    [SerializeField]
    protected int patrolIndex = 0;

    [SerializeField]
    protected float patrolStopRange = 0.2f;

    [SerializeField]
    protected Vector2 patrolWaitTime = new Vector2(1f, 2f);

    [SerializeField]
    protected float patrolWaitTime_Now = 0;


    [Header("AI Attack")]
    [SerializeField]
    [Range(0f,90f)]
    private float visionConeDegree = 45f;

    [SerializeField]
    protected float attackRange = 5f;

    [SerializeField]
    float attackDuration = 1;

    [SerializeField]
    float attackCooldown = 2;


    [Header("Other Components")]
    [SerializeField]
    GameObject playerGO;

    [SerializeField]
    protected NavMeshAgent navMeshAgent;

    [SerializeField]
    PatrolManager patrolManager;

    protected PatrolManager PatrolManager
    {
        get => patrolManager;
        set => patrolManager = value;
    }

    [SerializeField]
    protected Transform head;


    // Start is called before the first frame update
    void Start()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
        navMeshAgent = GetComponent<NavMeshAgent>();
        SetNewPatrolPoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (lastThinkTime + thinkRate <= Time.time)
        {
            AIThink();
            lastThinkTime = Time.time;
        }

        AIBehaviour();
    }

    public void SetActive(bool b)
    {
        this.enabled = b;
        navMeshAgent.enabled = b;
        if (!b)
        {
            ChangeState(AIState.Idle);
        }
    }


    public virtual void ChangeState(AIState newState)
    {
        previousState = currentState;
        currentState = newState;

        Debug.Log($"AI: {name}: Change State: {previousState} --> {currentState}");

        switch (previousState)
        {
            case AIState.Idle:
                break;
            case AIState.MoveToPatrol:
                break;
            case AIState.MoveToPlayer:
                EndState_MoveToPlayer();
                break;
            case AIState.Attack:
                break;

        }
        
        switch (currentState)
        {
            case AIState.Idle:
                break;
            case AIState.MoveToPatrol:
                ChangeState_MoveToPatrol();
                break;
            case AIState.MoveToPlayer:
                ChangeState_MoveToPlayer();
                break;
            case AIState.Attack:
                ChangeState_Attack();
                break;
        }
    }

    /// <summary>
    /// Process information on the tick rate
    /// </summary>
    protected virtual void AIThink()
    {
        //Debug.Log("I think");

        switch (currentState)
        {
            case AIState.Idle:
                if (GetDistanceToPlayer() <= attackRange)
                {
                    ChangeState(AIState.Attack);
                }
                else if (GetDistanceToPlayer() <= detectionRange && LineOfSight())
                {
                    ChangeState(AIState.MoveToPlayer);
                }
                else
                {
                    ChangeState(AIState.MoveToPatrol);
                }

                break;
            case AIState.MoveToPatrol:
                if (GetDistanceToPlayer() <= attackRange)
                {
                    ChangeState(AIState.Attack);
                }
                else if (GetDistanceToPlayer() <= detectionRange && LineOfSight())
                {
                    ChangeState(AIState.MoveToPlayer);
                }
                else
                {
                    AIThink_MoveToPatrol();
                }

                break;
            case AIState.MoveToPlayer:
                if (GetDistanceToPlayer() <= attackRange)
                {
                    ChangeState(AIState.Attack);
                }
                else if (GetDistanceToPlayer() <= detectionRange)
                {
                    AIThink_MoveToPlayer();
                }
                else
                {
                    ChangeState(AIState.MoveToPatrol);
                }

                break;
            case AIState.Attack:
                break;
        }
    }

    /// <summary>
    /// process information every frame
    /// </summary>
    protected virtual void AIBehaviour()
    {
        switch (currentState)
        {
            case AIState.Idle:
                break;
            case AIState.MoveToPatrol:
                AIBehaviour_MoveToPatrol();
                break;
            case AIState.MoveToPlayer:
                AIBehaviour_MoveToPlayer();
                break;
            case AIState.Attack:
                break;
        }
    }

    protected virtual float GetDistanceToPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, PlayerHandlerScript.current.transform.position);
        if (PlayerHandlerScript.IgnorePlayer)
        {
            distanceToPlayer = 1000f;
        }
        return distanceToPlayer;
    }


    protected virtual void ChangeState_MoveToPlayer()
    {
        navMeshAgent.SetDestination(playerGO.transform.position);
        playerPos = navMeshAgent.destination;
    }

    protected virtual void AIThink_MoveToPlayer()
    {
        if (LineOfSight())
        {
            navMeshAgent.SetDestination(playerGO.transform.position);
            playerPos = navMeshAgent.destination;
            if (moveToPlayerWaitTime_Now < moveToPlayerWaitTime.x)
            {
                moveToPlayerWaitTime_Now = Random.Range(moveToPlayerWaitTime.x, moveToPlayerWaitTime.y);
            }
        }

        if (moveToPlayerWaitTime_Now <= 0)
        {
            ChangeState(AIState.Idle);
        }
    }


    protected virtual void AIBehaviour_MoveToPlayer()
    {
        if (Vector3.Distance(transform.position, playerPos) < attackRange)
        {
            moveToPlayerWaitTime_Now -= Time.deltaTime;
            
        }
    }

    protected virtual void EndState_MoveToPlayer()
    {
        
    }

    protected virtual void SetNewPatrolPoint()
    {
        int temp = patrolManager.GetRandomPatrolIndex();
        while (temp.Equals(patrolIndex))
        {
            temp = patrolManager.GetRandomPatrolIndex();
        }

        patrolIndex = temp;
        Debug.Log($"New Patrol point {patrolIndex}: {patrolPos}");
        ResumePatrolPoint();
    }

    protected virtual void OverridePatrolPoint(Vector3 position)
    {
        SetNavAgent(position);
    }

    protected virtual void ResumePatrolPoint()
    {
        try
        {
            SetNavAgent(patrolManager.GetPatrol(patrolIndex).position);
        }
        catch (Exception e)
        {
            SetNewPatrolPoint();
        }
    }

    protected virtual void SetNavAgent(Vector3 position)
    {
        navMeshAgent.SetDestination(position);
        patrolPos = navMeshAgent.destination;
        patrolWaitTime_Now = Random.Range(patrolWaitTime.x, patrolWaitTime.y);
    }

    protected virtual void ChangeState_MoveToPatrol()
    {
        ResumePatrolPoint();
    }

    protected virtual void AIThink_MoveToPatrol()
    {
        if (patrolWaitTime_Now <= 0)
        {
            SetNewPatrolPoint();
        }
    }

    protected virtual void AIBehaviour_MoveToPatrol()
    {
        if (Vector3.Distance(transform.position, patrolPos) < patrolStopRange)
        {
            patrolWaitTime_Now -= Time.deltaTime;
        }
    }


    protected virtual void ChangeState_Attack()
    {
        navMeshAgent.SetDestination(transform.position);
        StartAttack();
    }

    public virtual void StartAttack()
    {
        Debug.Log("AI attack");
        StartCoroutine(AttackCoroutine());
    }

    public virtual void EndAttack()
    {
        ChangeState(AIState.Idle);
        lastThinkTime += attackCooldown;
    }

    protected virtual IEnumerator AttackCoroutine()
    {
        GameManagerScript.SetGameOver();
        yield return new WaitForSeconds(attackDuration);
        EndAttack();
    }


    protected virtual bool LineOfSight()
    {

        if (!IsInCone())
        {
            return false;
        }
        if (Physics.Raycast(head.position, playerGO.transform.position + playerOffset - head.position,
            out RaycastHit hit, detectionRange, LOSLayer))
        {
            // print($"Enemy raycast hit something {hit.collider.name}");
            if (hit.collider.CompareTag("Player"))
            {
                if (PlayerHandlerScript.IgnorePlayer)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        return false;
    }
    bool IsInCone()
    {
        // print($"{Vector3.Dot(head.forward, (playerGO.transform.position + head.position - playerOffset).normalized)}, {Math.Cos(visionConeDegree)}");
        if (Vector3.Dot(head.forward, (playerGO.transform.position + playerOffset - head.position).normalized) > Mathf.Abs( Mathf.Cos(visionConeDegree)))
        {
            return true;
        }
        return false;

    }

}