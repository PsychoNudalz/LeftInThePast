using System;
using System.Collections;
using System.Collections.Generic;
using QFSW.QC;
using UnityEngine;

/// <summary>
/// controls all dimension
/// </summary>
public class DimensionController : MonoBehaviour
{
    [SerializeField] PlayerHandlerScript player = PlayerHandlerScript.current;
    [SerializeField] int dimensionPointer = 0;
    [SerializeField] Dimension[] dimensions;
    [SerializeField] Dimension currentDimension;

    public Dimension CurrentDimension => currentDimension;


    public static DimensionController Current;
    // Start is called before the first frame update


    private void Awake()
    {
        Current = this;
        if (dimensions.Length == 0)
        {
            dimensions = GetComponentsInChildren<Dimension>();
        }
    }
    void Start()
    {
        currentDimension = dimensions[dimensionPointer];
        player = PlayerHandlerScript.current;
        currentDimension.TeleportPlayerTo();

        
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    /// <summary>
    /// teleport to the next dimension
    /// </summary>
    /// <param name="maskName">name of the teleport mask effect animation</param>
    public void LoopTeleport(string maskName)
    {
        currentDimension.TeleportPlayerFrom(maskName);

        dimensionPointer++;
        dimensionPointer = dimensionPointer % dimensions.Length;
        Vector3 transformOffset = dimensions[dimensionPointer].transform.position - currentDimension.transform.position;
        currentDimension = dimensions[dimensionPointer];
        player.TeleportMovePlayer(transformOffset);
        currentDimension.TeleportPlayerTo();

    }

    /// <summary>
    /// teleport to a set dimension
    /// </summary>
    /// <param name="index">index of dimension</param>
    /// <param name="maskName">name of the teleport mask effect animation</param>
    public void Teleport(int index, string maskName)
    {
        if (index < 0 || index > dimensions.Length - 1)
        {
            Debug.LogError($"Dimension index: {index} out of range {dimensions.Length}");
            return;
        }
        currentDimension.TeleportPlayerFrom(maskName);
        
        //Updating to new dimension
        dimensionPointer = index;
        Vector3 transformOffset = GetZDiff(dimensionPointer);
        currentDimension = dimensions[dimensionPointer];
        player.TeleportMovePlayer(transformOffset);
        currentDimension.TeleportPlayerTo();

    }

    public Vector3 GetZDiff(int i)
    {
        return dimensions[i].transform.position - currentDimension.transform.position;
    }

    public Vector3 GetZDiff(Dimension d)
    {
        return GetZDiff(currentDimension,d);
    }
    
    public static Vector3 GetZDiff(Dimension c,Dimension d)
    {
        return d.transform.position - c.transform.position;
    }

    public Dimension GetDimension(int i)
    {
        try
        {
            return dimensions[i];
        }
        catch (IndexOutOfRangeException e)
        {
            Debug.LogError(e);
            return null;
        }
    }

    public static List<RenderTexture> GetAllDimensionTextures()
    {
        List<RenderTexture> temp = new List<RenderTexture>();
        foreach (Dimension dimension in Current.dimensions)
        {
            temp.Add(dimension.GetDimensionTexture());
        }

        return temp;
    }

    [Command()]
    public static void ShowCurrentDimension()
    {
        if (Current?.currentDimension)
        {
            Debug.Log(Current.currentDimension);
        }
        else
        {
            Debug.LogError("Current dimension is null");
        }
    }

    [Command()]
    public static void TeleportDimension(int i = 4)
    {
        Current.Teleport(i,"TPEffect1");
    }
}
