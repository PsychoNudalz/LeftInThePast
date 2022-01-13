using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// spawns the monster in for one time
/// </summary>
public class InitialSpawnForEnemy : MonoBehaviour
{
    [SerializeField]
    private MonsterHandlerScript currentMonster;

    [SerializeField]
    private bool flag = false;

    private void Start()
    {
        currentMonster = MonsterHandlerScript.current;
    }

    [ContextMenu("Spawn Monster")]
    public void SpawnMonster()
    {
        if (!flag)
        {
            currentMonster.TeleportDimension(DimensionController.Current.CurrentDimension,transform.position,2f);
            flag = true;
        }
    }
}
