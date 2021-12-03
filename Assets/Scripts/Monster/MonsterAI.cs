using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

public class MonsterAI : EnemyAI
{
    [Header("Monster AI")]
    private SourceOfInterest currentSource;

    [FormerlySerializedAs("stareDuration")]
    [Header("Monster Stare")]
    [SerializeField]
    private float stareTime = 5f;

    private float stareTime_Now = 0;

    [SerializeField]
    private bool isEyeOpened = false;

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


    public SourceOfInterest CurrentSource => currentSource;


    public void RecieveSOI(SourceOfInterest newSource)
    {
        
        if (currentSource == null&&!(currentState.Equals(AIState.MoveToPlayer)||currentState.Equals(AIState.Attack)))
        {
            SetCurrentSource(newSource);
        }
        else
        {
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
                break;

        }
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
                ChangeState_Stare();
                break;
            case AIState.Investigate:
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
                break;

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
        head.LookAt(new Vector3(playerPos.x, Mathf.Max(head.position.y, playerPos.y),
            playerPos.z));
    }

    //Stare
    protected virtual void ChangeState_Stare()
    {
        stareTime_Now = stareTime;
        BroadcastMessage("OnStare_Start");
    }

    protected virtual void EndState_Stare()
    {
        currentSource = null;
    }

    protected virtual void AIThink_Stare()
    {
        if (stareTime_Now <= 0)
        {
            currentSource = null;
            BroadcastMessage("OnStart_End");

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
            ChangeState(AIState.MoveToPlayer);
        }
    }

    void SetCurrentSource(SourceOfInterest newSource)
    {
        currentSource = newSource;
        head.LookAt(new Vector3(currentSource.Position.x, Mathf.Max(head.position.y, currentSource.Position.y),
            currentSource.Position.z));

        switch (newSource.SourceOfInterestType)
        {
            case SourceOfInterestType.Noise:

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
        if ((stareTime_Now < stareTime-stareDelay&&currentState.Equals(AIState.Stare))||currentState.Equals(AIState.MoveToPlayer))
        {
            return base.LineOfSight();
        }

        else
        {
            return false;
        }
    }
}