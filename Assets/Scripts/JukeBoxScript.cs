using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JukeBoxScript : MonoBehaviour
{

    public void TeleportPlayer()
    {
        DimensionController.Current.LoopTeleport();
    }
    public void TeleportPlayer(int index)
    {
        DimensionController.Current.Teleport(index);
    }
}
