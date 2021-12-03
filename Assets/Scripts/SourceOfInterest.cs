using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SourceOfInterestType
{
    Noise,
    Tentacle
}

public class SourceOfInterest
{
    private string SourceItem;


    private Vector3 position;

    private SourceOfInterestType sourceOfInterestType;

    private float range;
    
    public string SourceItem1 => SourceItem;

    public Vector3 Position => position;

    public float Range => range;

    public SourceOfInterestType SourceOfInterestType => sourceOfInterestType;

    public SourceOfInterest(string sourceItem, Vector3 position, SourceOfInterestType sourceOfInterestType, float range)
    {
        SourceItem = sourceItem;
        this.position = position;
        this.sourceOfInterestType = sourceOfInterestType;
        this.range = range;
    }
}