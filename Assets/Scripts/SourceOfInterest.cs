using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

/// <summary>
/// SOI type
/// </summary>
public enum SourceOfInterestType
{
    Noise,
    Tentacle
}

/// <summary>
/// SOI creates a stimuli that maks the monster react
/// Noise to make it move to an area to investigate
/// Tentacle to cause it to stare when something touches the tentacle
/// </summary>
[Serializable]
public class SourceOfInterest
{
    private string sourceItem;


    private Vector3 position;

    private SourceOfInterestType sourceOfInterestType;

    private float range;

    public string SourceItem => sourceItem;

    public Vector3 Position => position;

    public float Range => range;

    public SourceOfInterestType SourceOfInterestType => sourceOfInterestType;

    public SourceOfInterest(string sourceItem, Vector3 position, SourceOfInterestType sourceOfInterestType, float range)
    {
        this.sourceItem = sourceItem;
        this.position = position;
        this.sourceOfInterestType = sourceOfInterestType;
        this.range = range;
    }

    public bool InRange(Vector3 pp)
    {
        return Vector3.Distance(pp, position) < range;
    }

    /// <summary>
    /// compare if the current SOI is closer than the other one
    /// true if closer
    /// </summary>
    /// <param name="pp"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool CompareRange(Vector3 pp, SourceOfInterest other)
    {
        return Vector3.Distance(pp, position) < Vector3.Distance(pp, other.position);
    }
}