using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform startingPos, endingPos;
    [SerializeField] private float platformSpeed;
    [SerializeField] private float distanceToReturn;
    [SerializeField] private bool goingToEnd;

    private void Start()
    {
        transform.position = startingPos.position;
        goingToEnd = true;
    }

    private void FixedUpdate()
    {
        if (goingToEnd)
        {
            // Return to start.
            if (Vector2.Distance(transform.position, endingPos.position) < distanceToReturn)
            {
                goingToEnd = false;
                return;
            }
            
            // Go to end.
            Vector3 smoothedPostion = Vector3.MoveTowards(transform.position, endingPos.position, platformSpeed * Time.deltaTime);
            transform.position = smoothedPostion;
        }
        else
        {
            // Return to start.
            if (Vector2.Distance(transform.position, startingPos.position) < distanceToReturn)
            {
                goingToEnd = true;
                return;
            }

            // Go to end.
            Vector3 smoothedPostion = Vector3.MoveTowards(transform.position, startingPos.position, platformSpeed * Time.deltaTime);
            transform.position = smoothedPostion;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        col.gameObject.transform.SetParent(transform);
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        col.gameObject.transform.SetParent(null);
    }
}
