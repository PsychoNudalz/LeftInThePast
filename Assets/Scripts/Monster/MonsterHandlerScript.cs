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

    [SerializeField]
    private TentaclesHandler tentaclesHandler;

    [Header("Current Dimension")]
    [SerializeField]
    private Dimension currentDimension;

    public Dimension CurrentDimension => currentDimension;

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

        if (!tentaclesHandler)
        {
            tentaclesHandler = GetComponentInChildren<TentaclesHandler>();
        }

        if (!monsterEffects.TentaclesHandler)
        {
            monsterEffects.TentaclesHandler = tentaclesHandler;
        }


        current = this;
    }

    private void Start()
    {
        if (!currentDimension)
        {
            currentDimension = DimensionController.Current.CurrentDimension;
        }

        TeleportDimension(currentDimension, 0f);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void RecieveSoi(SourceOfInterest newSource)
    {
        monsterAI.RecieveSoi(newSource);
    }

    public void SetAIActive(bool b)
    {
        monsterAI.SetActive(b);
    }

    public void EnableAI()
    {
        SetAIActive(true);
    }

    public void DisableAI()
    {
        SetAIActive(false);
    }

    public void TeleportDimension(Dimension d, float f = 3f)
    {
        if (f != 0)
        {
            StartCoroutine(teleportDimensionEnumerator(d, f,
                transform.position + DimensionController.GetZDiff(currentDimension, d)));
        }
        else
        {
            DisableAI();

            PostTeleport(d, transform.position + DimensionController.GetZDiff(currentDimension, d));
        }
    }

    public void TeleportDimension(Dimension d, Vector3 position, float f = 3f)
    {
        if (f != 0)
        {
            StartCoroutine(teleportDimensionEnumerator(d, f,
                position));
        }
        else
        {
            DisableAI();
            PostTeleport(d, position);
        }
    }


    IEnumerator teleportDimensionEnumerator(Dimension d, float f, Vector3 position)
    {
        monsterEffects.StartDespawnEffect();
        DisableAI();
        yield return new WaitForSeconds(f);
        Dimension temp = currentDimension;
        PostTeleport(d, position);
        yield return new WaitForSeconds(0f);
        //temp.SetDimensionActive(false);


        //AI reenable in animation
    }

    private void PostTeleport(Dimension d, Vector3 position)
    {
        transform.position = position;
        
        currentDimension = d;
        currentDimension.SetDimensionActive(true);

        monsterAI.ChangeState(AIState.Idle);
        monsterEffects.StartSpawnEffect();
    }

    public static void SetActive(bool b)
    {
        current.gameObject.SetActive(b);
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

    [Command()]
    public static void Monster_TeleportToPlayerDimension()
    {
        if (!current)
        {
            Debug.LogError("current null");
        }

        if (!DimensionController.Current.CurrentDimension)
        {
            Debug.LogError("current dimension null");
        }

        current.TeleportDimension(DimensionController.Current.CurrentDimension);
    }
}