using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{

    float timer = 0f;
    float timerMax = 5f;
    ArrayList ghostArray = new ArrayList();

    public void Update()
    {
        timer += Time.deltaTime;
        if (timer > timerMax)
        {
            timer = 0.0f;

            // ADD POSITION TO ARRAY
            ghostArray.Add("position" + transform.position);

            Debug.Log(ghostArray + " | ");


        }
        
    }
}
