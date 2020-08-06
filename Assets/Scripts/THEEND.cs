using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class THEEND : MonoBehaviour
{
    private void Start()
    {

    }

    public void ENDGAME()
    {
        Application.Quit();
        Debug.Log("I Quit :)");
    }
}
