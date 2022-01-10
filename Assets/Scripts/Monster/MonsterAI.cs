using System;
using System.Collections;
using System.Collections.Generic;
using QFSW.QC;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

public class MonsterAI : EnemyAI
{
    [Header("Monster AI")]
    [SerializeField]
     SourceOfInterest currentSource = new SourceOfInterest("", new Vector3(), SourceOfInterestType.Noise, 0f);

     [SerializeField]
     private int pathError_Max = 10;

     [SerializeField]
     private int pathError = 0;
     
    [FormerlySerializedAs("stareDuration")]
    [Header("Monster Stare")]
    [SerializeField]
    private float stareTime = 5f;

    private float stareTime_Now = 0;

    [SerializeField]
    private bool isEyeOpened = false;

    [SerializeField]
    private List<AIState> statesToStare = new List<AIState>();


    public bool IsEyeOpened
    {
        get => isEyeOpened;
        set => isEyeOpened = value;
    }

    [Header("Monster Chase")]
    [SerializeField]
    private float chaseSpeedMultiplier = 1.2f;

    [SerializeField]
    private float stareDelay = 1;


    [Header("Investigate Time")]
    [SerializeField]
    private Vector2 investigateWaitTime = new Vector2(1f, 2f);

    [SerializeField]
    private float investigateWaitTime_Now = 0;

    [SerializeField]
    private float investigateRange = 2f;

    [Header("Debugger")]
    [SerializeField]
    private bool showDebug = false;

    public bool ShowDebug
    {
        get => showDebug;
        set => showDebug = value;
    }

    [SerializeField]
    private float debugSphereRadius = .3f;

    public SourceOfInterest CurrentSource => currentSource;


    private void OnDrawGizmosSelected()
    {
        if (showDebug)
        {
            if (currentSource?.SourceItem != null)
            {
                Gizmos.DrawSphere(currentSource.Position, debugSphereRadius);
            }
            Gizmos.DrawSphere(patrolPos,patrolStopRange);

        }
    }


    public void RecieveSoi(SourceOfInterest newSource)
    {
        if (currentState.Equals(AIState.MoveToPlayer) || currentState.Equals(AIState.Attack))
        {
            return;
        }

        if (!newSource.InRange(transform.position))
        {
            return;
        }

        if (!enabled)
        {
            return;
        }

        if (currentSource == null || (currentSource.SourceItem == null))
        {
            SetCurrentSource(newSource);
        }
        else
        {
            switch (newSource.SourceOfInterestType)
            {
                case SourceOfInterestType.Noise:
                    if (newSource.CompareRange(transform.position, currentSource))
                    {
                        SetCurrentSource(newSource);
                    }

                    break;
                case SourceOfInterestType.Tentacle:
                    if (currentSource.SourceOfInterestType.Equals(SourceOfInterestType.Noise))
                    {
                        SetCurrentSource(newSource);
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public override void ChangeState(AIState newState)
    {
        base.ChangeState(newState);
        switch (previousState)
        {
            case AIState.Stare:
                EndState_Stare();
                break;
            case AIState.Investigate:
                EndState_Investigate();
                break;
        }

        switch (currentState)
        {
            case AIState.Idle:
                break;
            case AIState.MoveToPatrol:
                PatrolManager = MonsterHandlerScript.current.CurrentDimension.PatrolManager;
                break;
            case AIState.MoveToPlayer:

                break;
            case AIState.Attack:
                break;
            case AIState.Stare:
                ChangeState_Stare();
                break;
            case AIState.Investigate:
                ChangeState_Investigate();
                break;
        }
    }

    protected override void AIThink()
    {
        base.AIThink();
        switch (currentState)
        {
            case AIState.Idle:
                break;
            case AIState.MoveToPatrol:
                break;
            case AIState.MoveToPlayer:
                break;
            case AIState.Attack:
                break;
            case AIState.Stare:
                AIThink_Stare();
                break;
            case AIState.Investigate:
                AIThink_Investigate();
                break;
        }
    }


    protected override void AIBehaviour()
    {
        base.AIBehaviour();
        switch (currentState)
        {
            case AIState.Idle:
                break;
            case AIState.MoveToPatrol:
                break;
            case AIState.MoveToPlayer:
                break;
            case AIState.Attack:
                break;
            case AIState.Stare:
                AIBehaviour_Stare();
                break;
            case AIState.Investigate:
                AIBehaviour_Investigate();
                break;
        }
    }

    //Move To Patrol
    protected override void AIThink_MoveToPatrol()
    {
        base.AIThink_MoveToPatrol();
        if (!navMeshAgent.hasPath)
        {
            pathError++;
            if (pathError > pathError_Max)
            {
                if (showDebug)
                {
                    Debug.Log($"Monster can't path to {patrolIndex}");
                }

                PatrolManager = MonsterHandlerScript.current.CurrentDimension.PatrolManager;
                SetNewPatrolPoint();
                pathError = 0;
            }
        }
    }

    //Move To Player
    protected override void ChangeState_MoveToPlayer()
    {
        base.ChangeState_MoveToPlayer();
        BroadcastMessage("OnChase_Start");
    }

    protected override void EndState_MoveToPlayer()
    {
        base.EndState_MoveToPlayer();
        BroadcastMessage("OnChase_End");
        currentSource = null;
    }

    protected override void AIBehaviour_MoveToPlayer()
    {
        base.AIBehaviour_MoveToPlayer();
        // head.LookAt(new Vector3(playerPos.x, Mathf.Max(head.position.y, playerPos.y),
        //     playerPos.z));
        head.LookAt(PlayerHandlerScript.current.GetHead().transform.position);
    }

    //Stare
    protected virtual void ChangeState_Stare()
    {
        if (currentSource != null)
        {
            head.LookAt(new Vector3(currentSource.Position.x, Mathf.Max(head.position.y, currentSource.Position.y),
                currentSource.Position.z));
        }

        stareTime_Now = stareTime;
        BroadcastMessage("OnStare_Start");
    }

    protected virtual void EndState_Stare()
    {
    }

    protected virtual void AIThink_Stare()
    {
        if (stareTime_Now <= 0)
        {
            currentSource = null;
            BroadcastMessage("OnStare_End");

            ChangeState(AIState.MoveToPatrol);
        }
        else
        {
        }
    }

    protected virtual void AIBehaviour_Stare()
    {
        stareTime_Now -= Time.deltaTime;
        if (LineOfSight())
        {
            if (showDebug)
            {
                Debug.Log("Monster sees player");
            }
            currentSource = null;
            ChangeState(AIState.MoveToPlayer);
        }
        else
        {
            if (showDebug)
            {
                //Debug.Log("Monster can't sees player");
            }
        }
    }

    protected virtual void ChangeState_Investigate()
    {
        SetNavAgent(currentSource.Position);
    }

    protected virtual void EndState_Investigate()
    {
    }

    protected virtual void AIThink_Investigate()
    {
        if (Vector3.Distance(transform.position, currentSource.Position) < investigateRange)
        {
            SetNavAgent(transform.position);
            ChangeState(AIState.Stare);
        }
    }

    protected virtual void AIBehaviour_Investigate()
    {
    }


    void SetCurrentSource(SourceOfInterest newSource)
    {
        currentSource = newSource;


        switch (newSource.SourceOfInterestType)
        {
            case SourceOfInterestType.Noise:
                //OverridePatrolPoint(newSource.Position);
                ChangeState(AIState.Investigate);
                break;
            case SourceOfInterestType.Tentacle:
                OverridePatrolPoint(transform.position);
                ChangeState(AIState.Stare);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    protected override bool LineOfSight()
    {
        if ((stareTime_Now < stareTime - stareDelay && statesToStare.Contains(currentState)))
        {
            return base.LineOfSight();
        }

        else
        {
            return false;
        }
    }


    protected override float GetDistanceToPlayer()
    {
        float distanceToPlayer = base.GetDistanceToPlayer();
        if (PlayerHandlerScript.IgnorePlayer)
        {
            distanceToPlayer = 1000f;
        }

        return distanceToPlayer;
    }
    



}