using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialOverride : MonoBehaviour
{
    [SerializeField]
    private Material material;

    [SerializeField]
    private Renderer[] renderers;
    // Start is called before the first frame update
    void Start()
    {
        SetRenderers();
    }

    private void SetRenderers()
    {
        if (renderers.Length == 0)
        {
            renderers = GetComponentsInChildren<Renderer>();
        }
    }

    [ContextMenu("Apply Materials")]
    public void SetAllMaterials()
    {
        SetRenderers();
        foreach (Renderer renderer1 in renderers)
        {
            renderer1.material = material;
        }
    }
}
