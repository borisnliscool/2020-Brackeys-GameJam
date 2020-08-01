using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GhostController ghost;
    public PlayerMovement pMovement;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            StartRewind();
        }    
    }

    void StartRewind()
    {
        pMovement.tpToStart();
        ghost.StartGhost();
    }
}
