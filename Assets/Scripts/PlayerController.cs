using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public delegate void PlayerDeath();
    public event PlayerDeath PlayerDied;

    public GhostController ghost;
    public PlayerMovement pMovement;
    public EventManager eventMan;

    private void Start()
    {
        ghost = FindObjectOfType<GhostController>();
        pMovement = FindObjectOfType<PlayerMovement>();
        eventMan = FindObjectOfType<EventManager>();
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
        
        if(Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Reloading scene");
            eventMan.ReloadScene();
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
