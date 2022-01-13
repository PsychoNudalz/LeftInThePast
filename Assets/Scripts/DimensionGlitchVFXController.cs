using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
/// <summary>
/// controls and sets the texture of the dimension glitch
/// </summary>
public class DimensionGlitchVFXController : MonoBehaviour
{

    [SerializeField]
    private VisualEffect vfx;

    [SerializeField]
    private int dimensionIndex = -1;

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
        if (dimensionIndex >= 0)
        {
            connectedDimention = DimensionController.Current.GetDimension(dimensionIndex);
        }
        if (connectedDimention)
        {
            vfx.SetTexture("RenderTexture",connectedDimention.GetDimensionTexture());
        }
    }


    public void Play()
    {
        vfx.Play();
    }
    public void Stop()
    {
        //vfx.Reinit();
        vfx.Stop();
    }
}
