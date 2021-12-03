using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHandlerScript : MonoBehaviour
{

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
