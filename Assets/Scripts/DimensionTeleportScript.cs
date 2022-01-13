using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// script for teleporting the player
/// </summary>
public class DimensionTeleportScript : MonoBehaviour
{

    [Tooltip("name of the teleport animation")]
    [SerializeField]
    private string maskName = "TPEffect1";

    public void TeleportPlayer()
    {
        DimensionController.Current.LoopTeleport(maskName);
    }
    /// <summary>
    /// teleport to a selected dimension
    /// </summary>
    /// <param name="index"></param>
    public void TeleportPlayer(int index)
    {
        DimensionController.Current.Teleport(index, maskName);
    }
}
