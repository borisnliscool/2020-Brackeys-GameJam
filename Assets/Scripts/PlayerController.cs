using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public delegate void PlayerDeath();
    public event PlayerDeath PlayerDied;

    public GhostController ghost;
    public PlayerMovement pMovement;
    private void Start()
    {
        ghost = FindObjectOfType<GhostController>();
        pMovement = FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        if (FreezeTime.i.timeFroze)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            StartRewind();
        }    
    }

    private void StartRewind()
    {
        ghost.StartNewGhost();
    }



    public void Die()
    {
        PlayerDied?.Invoke();
        Destroy(gameObject);
    }
}
