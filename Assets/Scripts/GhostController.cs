using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    float timer = 0f;
    float timerMax = 1f;
    List<Vector3> ghostArray = new List<Vector3>();
    public Transform ghostPrefab;
    public Transform ghost;
    public int count = 0;
    public bool ghostStarted = false;
    public Transform player;

    public void Update()
    {
        if(!ghostStarted)
        {
            ghostArray.Add(player.transform.position);
        }

        if (ghostStarted)
        {
            // TODO: FIX INDEX OUT OF BOUNDS
            ghost.position = ghostArray[count];
            count++;
        }
    }

    public void StartGhost()
    {
        if(!ghostStarted)
        {
            Debug.Log("ghost starting!");

            ghost = Instantiate(ghostPrefab, ghostArray[0], Quaternion.identity);

            ghostStarted = true;
        }
        
    }
}
