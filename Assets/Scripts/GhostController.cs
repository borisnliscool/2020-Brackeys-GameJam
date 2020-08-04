using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    // Variables
    private List<Vector2> ghostArray = new List<Vector2>();
    public Ghost ghostPrefab;
    public Ghost ghost;
    public int count = 0;
    public bool ghostStarted = false;
    public PlayerMovement playerM;

    private void Start()
    {
        playerM = FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        if(!ghostStarted)
            ghostArray.Add(playerM.transform.position);

        if (ghostStarted)
        {
            // Guard Clause.
            if (ghostArray.Count <= count)
                return;

            ghost.transform.position = ghostArray[count];
            //ghost.rB.AddForce(new Vector2(0, ghostArray[count].y) * Time.deltaTime * 9.81f);
            
            count++;
        }
    }

    public void StartGhost()
    {
        if(!ghostStarted)
        {
            ghost = Instantiate(ghostPrefab, ghostArray[0], Quaternion.identity);

            ghostStarted = true;
        }
        
    }
}
