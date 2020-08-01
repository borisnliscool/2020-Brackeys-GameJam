using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{

    float timer = 0f;
    float timerMax = 5f;

    public void Update()
    {

        Debug.Log("ea");
        timer += Time.deltaTime;
        if (timer > timerMax)
        {
            timer = 0.0f;

            Debug.Log("helo");
        }
        
    }
}
