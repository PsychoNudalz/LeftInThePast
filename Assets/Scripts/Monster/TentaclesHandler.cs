using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentaclesHandler : MonoBehaviour
{

    [SerializeField]
    private Tentacle[] tentacles = Array.Empty<Tentacle>();

    [SerializeField]
    private List<string> tagList;

    public List<string> TagList => tagList;

    [ContextMenu("Awake")]
    private void Awake()
    {
        if (tentacles.Length ==0)
        {
            tentacles = GetComponentsInChildren<Tentacle>();
        }

        foreach (Tentacle tentacle in tentacles)
        {
            tentacle.TentaclesHandler = this;
        }
    }
}
