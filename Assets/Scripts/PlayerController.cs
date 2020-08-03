using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GhostController ghost;
    public PlayerMovement pMovement;
    private void Start()
    {
        ghost = FindObjectOfType<GhostController>();
        pMovement = FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            StartRewind();
        }    
    }

    private void StartRewind()
    {
        pMovement.TpToStart();
        ghost.StartGhost();
    }
}
