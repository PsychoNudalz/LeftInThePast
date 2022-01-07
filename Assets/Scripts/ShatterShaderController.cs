using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatterShaderController : MonoBehaviour
{
    private MeshRenderer[] meshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponentsInChildren<MeshRenderer>();
        List<RenderTexture> temp = DimensionController.GetAllDimensionTextures();
        if (temp.Count >= 4)
        {
            foreach (MeshRenderer meshRenderer1 in meshRenderer)
            {
                meshRenderer1.material.SetTexture("_RT1",temp[0]);
                meshRenderer1.material.SetTexture("_RT2",temp[1]);
                meshRenderer1.material.SetTexture("_RT3",temp[2]);
                meshRenderer1.material.SetTexture("_RT4",temp[3]);
            }
        }
        else
        {
            Debug.LogError("Shatter Shader Renter texture less than 4");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
