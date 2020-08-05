using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeTime : MonoBehaviour
{
    public bool timeFroze { get; private set; } = false;
    public static FreezeTime i;
    void Awake()
    {
        if (i != null)
        {
            Debug.LogError("More than one FreezeTime Script in play.");
            return;
        }
        else
        {
            i = this;
        }
    }

    public void TimeFreezeRequest()
    {
        if (timeFroze)
        {
            return;
        }

        timeFroze = true;
    }

    public void TimeUnfreezeRequest()
    {
        if (!timeFroze)
        {
            return;
        }

        timeFroze = false;
    }
}
