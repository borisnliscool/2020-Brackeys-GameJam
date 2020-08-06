using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    public delegate void PlayerToGhost();
    public event PlayerToGhost PlayerTurnedToGhost;
    public event PlayerToGhost PlayerCreatedGhost;
    // Variables
    private List<List<Vector2>> ghostArray = new List<List<Vector2>>();
    private List<int> ghostIndexPos = new List<int>();
    [SerializeField] private Ghost ghostPrefab;
    [SerializeField] private PlayerController playerPrefab;
    private Vector2 lastPosition;
    public List<Ghost> activeGhosts;
    public int listCount = 0;
    public PlayerMovement playerM;
    private bool trackingPlayer = true;

    private void Awake()
    {
        playerM = FindObjectOfType<PlayerMovement>();
        
    }

    private void Start()
    {
        ghostArray.Add(new List<Vector2>());
        ghostIndexPos.Add(0);
    }

    private void OnEnable()
    {
        playerM.GetComponent<PlayerController>().PlayerDied += GhostController_PlayerDied;
    }
    private void OnDestroy()
    {
        if (playerM == null)
        {
            return;
        }
        playerM.GetComponent<PlayerController>().PlayerDied -= GhostController_PlayerDied;
    }


    private void GhostController_PlayerDied()
    {
        trackingPlayer = false;
        playerM.GetComponent<PlayerController>().PlayerDied -= GhostController_PlayerDied;
    }

    private void Update()
    {
        if (!trackingPlayer)
        {
            return;
        }

        if (FreezeTime.i.timeFroze)
        {
            return;
        }

        ghostArray[listCount].Add(playerM.transform.position);
        
        

        for (int i = 0; i < activeGhosts.Count; i++)
        {
            ghostIndexPos[i]++;
            if (ghostIndexPos[i] >= ghostArray[i].Count)
            {
                continue;
            }

            activeGhosts[i].transform.position = ghostArray[i][ghostIndexPos[i]];
        }
    }

    public void StartNewGhost()
    {
        activeGhosts.Add(Instantiate(ghostPrefab, ghostArray[listCount][0], Quaternion.identity));
        ghostArray.Add(new List<Vector2>());
        ghostIndexPos.Add(0);
        listCount++;
        int ghostsCreated = PlayerPrefs.GetInt("Ghosts");
        PlayerPrefs.SetInt("Ghosts", ghostsCreated + 1);
        PlayerCreatedGhost?.Invoke();
    }

    public void PlayerDied()
    {
        if (activeGhosts.Count == 0)
        {
            playerM.GetComponent<PlayerController>().Die();
            return;
        }
        
        Ghost lastGhost = activeGhosts[activeGhosts.Count - 1];
        playerM.transform.position = lastGhost.transform.position;
        playerM.gameObject.SetActive(false);
        activeGhosts.Remove(lastGhost);
        Destroy(lastGhost.gameObject);
        PlayerTurnedToGhost?.Invoke();
        StartCoroutine(ReactivatePlayer());
    }

    private IEnumerator ReactivatePlayer()
    {
        yield return new WaitForFixedUpdate();
        playerM.gameObject.SetActive(true);
    }
}
