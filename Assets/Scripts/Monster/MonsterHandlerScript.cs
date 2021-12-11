using System.Collections;
using System.Collections.Generic;
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
}
