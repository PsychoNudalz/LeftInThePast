using System;
using System.Collections;
using System.Collections.Generic;
using QFSW.QC;
using UnityEngine;

/// <summary>
/// main script on controlling the monster, other scripts outside the monster will communicate with it via this
/// </summary>
public class MonsterHandlerScript : MonoBehaviour
{
    public static MonsterHandlerScript current;

    [SerializeField]
    private MonsterAI monsterAI;

    [SerializeField]
    private MonsterEffects monsterEffects;

    [SerializeField]
    private TentaclesHandler tentaclesHandler;

    private Coroutine teleportCoroutine;

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

    /// <summary>
    /// Teleport to a dimension
    /// </summary>
    /// <param name="d">dimension specified</param>
    /// <param name="f">time until it arrives at the new dimension</param>
    public void TeleportDimension(Dimension d, float f = 3f)
    {
        if (f != 0)
        {
            if (teleportCoroutine == null)
            {
                teleportCoroutine= StartCoroutine(teleportDimensionEnumerator(d, f,
                    transform.position + DimensionController.GetZDiff(currentDimension, d)));
            }
        }
        else
        {
            DisableAI();

            PostTeleport(d, transform.position + DimensionController.GetZDiff(currentDimension, d));
        }
    }

    /// <summary>
    /// Teleport to a dimension with a set global position
    /// </summary>
    /// <param name="d">dimension specified</param>
    /// <param name="position"> global position in the new dimension</param>
    /// <param name="f">time until it arrives at the new dimension</param>
    public void TeleportDimension(Dimension d, Vector3 position, float f = 3f)
    {
        if (f != 0)
        {
            if (teleportCoroutine == null)
            {
                teleportCoroutine= StartCoroutine(teleportDimensionEnumerator(d, f,
                    position));
            }
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
        teleportCoroutine = null;
        //yield return new WaitForSeconds(0f);
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

/// <summary>
/// Forcefully change the monster’s AI state.
/// </summary>
/// <param name="state"></param>
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
/// <summary>
/// teleports the monster to the player’s dimension. Forced = true to teleport to final dimension.
/// </summary>
/// <param name="forced"></param>
    [Command()]
    public static void Monster_TeleportToPlayerDimension(bool forced = false)
    {
        if (!current)
        {
            Debug.LogError("current null");
        }

        if (!DimensionController.Current.CurrentDimension)
        {
            Debug.LogError("current dimension null");
        }

        if (!DimensionController.Current.CurrentDimension.DimensionEnum.Equals(DimensionEnum.Final))
        {
            Debug.Log($"Teleporting Monster to {DimensionController.Current.CurrentDimension}");
            current.TeleportDimension(DimensionController.Current.CurrentDimension);
        }
    }
/// <summary>
/// Set if AI will print logs in the console.
/// </summary>
/// <param name="b"></param>
    [Command()]
    public static void MonsterAI_ShowDebug(bool b)
    {
        current.monsterAI.ShowDebug = b;
    } 
}