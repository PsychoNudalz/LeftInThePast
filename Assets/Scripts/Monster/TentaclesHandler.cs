using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentaclesHandler : MonoBehaviour
{

    [SerializeField]
    private Tentacle[] tentacles = Array.Empty<Tentacle>();

    [SerializeField]
    private List<string> playerTagList;
    [SerializeField]
    private List<string> itemTagList;
    public List<string> PlayerTagList => playerTagList;
    public List<string> ItemTagList => itemTagList;

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
