using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JukeBoxScript : MonoBehaviour
{

    public void TeleportPlayer()
    {
        DimensionController.current.LoopTeleport();
    }
    public void TeleportPlayer(int index)
    {
        DimensionController.current.Teleport(index);
    }
}
