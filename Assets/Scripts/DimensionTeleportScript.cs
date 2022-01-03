using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionTeleportScript : MonoBehaviour
{

    [SerializeField]
    private string maskName = "TPEffect1";

    public void TeleportPlayer()
    {
        DimensionController.Current.LoopTeleport(maskName);
    }
    public void TeleportPlayer(int index)
    {
        DimensionController.Current.Teleport(index, maskName);
    }
}
