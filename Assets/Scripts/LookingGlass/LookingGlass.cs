using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class LookingGlass : MonoBehaviour
{
    [SerializeField]
    private Transform modelParent;

    [SerializeField]
    private Transform originalPointsParent;

    [SerializeField]
    private List<Material> lookingGlassMaterials;

    private List<Transform> lookingGlassTransforms = new List<Transform>();
    private List<Transform> originalTransforms = new List<Transform>();

    [SerializeField]
    private float scaleDownSize = 0.9f;

    [Header("Animation")]
    [SerializeField]
    private float animationSpeed = 0.25f;

    [SerializeField]
    private float moveAmount = 1;

    [SerializeField]
    private float rotateAmount = 5f;

    private void Awake()
    {
        GameObject original = new GameObject("origin");
        foreach (Transform t in modelParent.GetComponentsInChildren<Transform>())
        {
            if (!t.Equals(transform))
            {
                t.localScale *= scaleDownSize;
                lookingGlassTransforms.Add(t);

                originalTransforms.Add(Instantiate(original, t.position, t.rotation,originalPointsParent).transform);

                if (t.TryGetComponent(out Renderer r))
                {
                    lookingGlassMaterials.Add(r.material);
                }
            }
        }
        Destroy(original);
    }


    private void Update()
    {
        UpdateLookingGlass();
    }

    public void SetLookingGlass(RenderTexture rt)
    {
        foreach (Material material in lookingGlassMaterials)
        {
            material.SetTexture("_MainText", rt);
        }
    }

    public void UpdateLookingGlass()
    {
        for (int i = 0; i < lookingGlassTransforms.Count; i++)
        {
            float offset = (float) i / lookingGlassMaterials.Count;
            float sinTime = (Time.time+ offset) * animationSpeed;
            lookingGlassTransforms[i].position = originalTransforms[i].position +
                                                 new Vector3(0, Mathf.Sin(sinTime)*moveAmount , 0);
            Vector3 rotationAmount = new Vector3(Mathf.Sin(sinTime), Mathf.Cos(sinTime), Mathf.Sin(sinTime)) *
                                     Time.deltaTime*rotateAmount;
            
            lookingGlassTransforms[i].Rotate(rotationAmount);
        }
    }
}