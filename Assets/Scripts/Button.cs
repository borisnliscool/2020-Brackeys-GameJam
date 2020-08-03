using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class Button : MonoBehaviour
{
    public delegate void ButtonSwitch();
    public event ButtonSwitch ButtonSwitchedOpen;
    public event ButtonSwitch ButtonSwitchedClosed;

    // Variables
    [SerializeField] private Sprite unpressedButton;
    [SerializeField] private Sprite pressedButton;
    private SpriteRenderer spriteRenderer;
    List<Collider2D> collidedObjects = new List<Collider2D>();

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        collidedObjects.Clear();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.name + " entered.");
        spriteRenderer.sprite = pressedButton;
        if (!collidedObjects.Contains(col))
        {
            collidedObjects.Add(col);
        }
        if (collidedObjects.Count == 1)
        {
            ButtonSwitchedOpen?.Invoke();
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        OnTriggerEnter2D(col);
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (collidedObjects.Count == 0)
        {
            Debug.Log(col.name + " exited.");
            spriteRenderer.sprite = unpressedButton;
            ButtonSwitchedClosed?.Invoke();
        }
    }
}
