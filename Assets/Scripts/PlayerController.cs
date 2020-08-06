using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public delegate void PlayerDeath();
    public event PlayerDeath PlayerDied;
    public event PlayerDeath PlayerGhostMove;

    public GhostController ghost;
    public PlayerMovement pMovement;
    public EventManager eventMan;
    public bool stopGhostSpawn;

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

        if (Input.GetKeyDown(KeyCode.G) && !stopGhostSpawn)
        {
            StartCoroutine(TutorialMoved());
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
        int deaths = PlayerPrefs.GetInt("Deaths");
        PlayerPrefs.SetInt("Deaths", deaths + 1);
    }

    IEnumerator TutorialMoved()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerGhostMove?.Invoke();
    }
}
