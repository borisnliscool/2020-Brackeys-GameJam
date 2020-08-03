using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public delegate void ButtonSwitch();
    public event ButtonSwitch ButtonSwitched;

    // Variables
    private bool playerStanding;

    private void Update()
    {
        if (ButtonSwitched != null)
            ButtonSwitched();
    }
}
