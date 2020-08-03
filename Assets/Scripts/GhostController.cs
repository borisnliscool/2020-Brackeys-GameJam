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
            ghostArray.Add(playerM.rb.velocity);

        if (ghostStarted)
        {
            // Guard Clause.
            if (ghostArray.Count <= count)
                return;

            ghost.rB.velocity += new Vector2(ghostArray[count].x * Time.deltaTime * 350, 0);
            ghost.rB.AddForce(new Vector2(0, ghostArray[count].y));
            
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
