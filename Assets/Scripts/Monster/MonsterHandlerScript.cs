using System;
using System.Collections;
using System.Collections.Generic;
using QFSW.QC;
using UnityEngine;

public class MonsterHandlerScript : MonoBehaviour
{
    public static MonsterHandlerScript current;

    [SerializeField]
    private MonsterAI monsterAI;

    [SerializeField]
    private MonsterEffects monsterEffects;
    // Start is called before the first frame update
    void Awake()
    {
        if (!monsterAI)
        {
            monsterAI = GetComponent<MonsterAI>();
        }

        if (!monsterEffects)
        {
            monsterEffects = GetComponent<MonsterEffects>();
        }

        current = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RecieveSoi(SourceOfInterest newSource)
    {
        monsterAI.RecieveSoi(newSource);
    }


    [Command()]
    public static void Monster_ChangeState(string state)
    {
        foreach (AIState aiState in Enum.GetValues(typeof(AIState)))
        {
            if (aiState.ToString().ToUpper().Contains(state.ToUpper()))
            {
                current.monsterAI.ChangeState(aiState);
            }
        }
    }
}
