using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private GhostController controller;
    private bool onCooldown;

    private void Awake()
    {
        controller = FindObjectOfType<GhostController>();
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (onCooldown)
        { 
            return;
        }

        if (col.GetComponent<PlayerController>() != null)
        {
            controller.PlayerDied();
            onCooldown = true;
            StartCoroutine(Cooldown());
        }
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(0.5f);
        onCooldown = false;
    }
}
