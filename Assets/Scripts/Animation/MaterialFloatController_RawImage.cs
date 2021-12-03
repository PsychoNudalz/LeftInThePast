using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MaterialFloatController_RawImage : MonoBehaviour
{
    [FormerlySerializedAs("renderer")]
    [SerializeField]
    private RawImage rawImage;

    [SerializeField]
    private Material material;

    [Header("Controller")]
    [SerializeField]
    private string propertyName;
    [SerializeField]
    [Range(0, 1f)]
    private float range = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (!rawImage)
        {
            rawImage = GetComponent<RawImage>();
        }

        if (!rawImage)
        {
            Debug.LogError($"failed to get render");
            return;
        }
        material = rawImage.material;
    }

    // Update is called once per frame
    void Update()
    {
        material.SetFloat(propertyName,range);
    }
}