using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Control The Slider of the shaders
/// </summary>
public class AnimatedShader : MonoBehaviour
{
    [Header("Controller")]
    [SerializeField]
    [Range(0f, 1f)]
    private float value;

    [SerializeField]
    private string controlledValueName = "_ControlValue";

    [SerializeField]
    private float animationTime = 1f;

    private bool played;
    
    [Header("Compnents")]
    [Tooltip("Make sure Materials have _ControlValue")]
    [SerializeField]
    private List<Renderer> renderers;

    private List<Material> materials = new List<Material>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (Renderer r in renderers)
        {
            materials.Add(r.material);
        }
        UpdateAllMaterial();
    }

    // Update is called once per frame
    void Update()
    {
        if (played)
        {
            value += (1 / animationTime) * Time.deltaTime;
            if (value >= 1f)
            {
                played = false;
                enabled = false;
            }
        }
        
        UpdateAllMaterial();
    }

    void UpdateAllMaterial()
    {
        foreach (Material m in materials)
        {
            m.SetFloat(controlledValueName,value);
        }
    }

    public void Play()
    {
        played = true;
        enabled = true;
    }
    
}
