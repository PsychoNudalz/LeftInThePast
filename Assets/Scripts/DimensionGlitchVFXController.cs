using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class DimensionGlitchVFXController : MonoBehaviour
{

    [SerializeField]
    private VisualEffect vfx;

    [SerializeField]
    private Dimension connectedDimention;
    // Start is called before the first frame update
    private void Awake()
    {
        if (!vfx)
        {
            vfx = GetComponent<VisualEffect>();
        }
    }

    void Start()
    {
        if (connectedDimention)
        {
            vfx.SetTexture("RenderTexture",connectedDimention.GetDimensionTexture());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
