using System.Collections;
using System.Collections.Generic;
using QFSW.QC;
using UnityEngine;

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
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void LoopTeleport()
    {
        currentDimension.TeleportPlayerFrom();

        dimensionPointer++;
        dimensionPointer = dimensionPointer % dimensions.Length;
        Vector3 transformOffset = dimensions[dimensionPointer].transform.position - currentDimension.transform.position;
        currentDimension = dimensions[dimensionPointer];
        player.TeleportMovePlayer(transformOffset);
        currentDimension.TeleportPlayerTo();

    }

    public void Teleport(int index)
    {
        if (index < 0 || index > dimensions.Length - 1)
        {
            Debug.LogError($"Dimension index: {index} out of range {dimensions.Length}");
            return;
        }
        currentDimension.TeleportPlayerFrom();
        
        //Updating to new dimension
        dimensionPointer = index;
        Vector3 transformOffset = GetZDiff(dimensionPointer);
        currentDimension = dimensions[dimensionPointer];
        player.TeleportMovePlayer(transformOffset);

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
}