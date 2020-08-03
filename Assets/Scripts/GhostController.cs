using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    // Variables
    private List<Vector3> ghostArray = new List<Vector3>();
    public Transform ghostPrefab;
    public Transform ghost;
    public int count = 0;
    public bool ghostStarted = false;
    public Transform player;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
    }

    private void Update()
    {
        if(!ghostStarted)
            ghostArray.Add(player.transform.position);

        if (ghostStarted)
        {
            // Guard Clause.
            if (ghostArray.Count <= count)
                return;
                
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
