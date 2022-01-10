using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingAndThunder : MonoBehaviour
{
    [SerializeField]
    private float maxIntensity;

    [SerializeField]
    private float minIntensity;

    [SerializeField]
    [Range(0f, 1f)]
    private float flashPoint;

    [SerializeField]
    private float frequency;

    [Header("Components")]
    [SerializeField]
    private Light[] lights = Array.Empty<Light>();

    [SerializeField]
    private Sound thunderSound;

    private bool wasLightning = false;


    [ContextMenu("Start")]
    // Start is called before the first frame update
    void Start()
    {
        if (lights.Length == 0)
        {
            lights = GetComponentsInChildren<Light>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLights();
    }

    void UpdateLights()
    {
        if (lights.Length == 0)
        {
            return;
        }

        float intensity = CalculateIntensity();
        if (!wasLightning &&!lights[0].intensity.Equals(intensity))
        {
            thunderSound.Play();
        }

        foreach (Light light1 in lights)
        {
            if (light1.intensity.Equals(intensity))
            {
                wasLightning = false;
                return;
            }

            light1.intensity = intensity;
        }

        wasLightning = lights[0].intensity.Equals(intensity);
    }

    float CalculateIntensity()
    {
        float temp = 0f;
        temp = Mathf.Abs(Mathf.Sin(Time.time * frequency));
        temp = Mathf.Max(flashPoint, temp);
        temp = (temp - flashPoint) / (1 - flashPoint);
        temp = temp * (maxIntensity - minIntensity) + minIntensity;

        return temp;
    }

    [ContextMenu("Set To Max")]
    public void SetToMax()
    {
        foreach (Light light1 in lights)
        {
            light1.intensity = maxIntensity;
        }
    }

    [ContextMenu("Set To Min")]
    public void SetToMin()
    {
        foreach (Light light1 in lights)
        {
            light1.intensity = minIntensity;
        }
    }
}