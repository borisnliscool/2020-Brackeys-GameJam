using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class ButtonScript : MonoBehaviour
{
    public delegate void ButtonSwitch();
    public event ButtonSwitch ButtonSwitchedOpen;
    public event ButtonSwitch ButtonSwitchedClosed;
    public event ButtonSwitch TutorialButtonUsed;
    

    // Variables
    [SerializeField] private Sprite unpressedButton;
    [SerializeField] private Sprite pressedButton;
    private bool buttonPressed = false;
    private bool tutorialButtonPress;
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
        spriteRenderer.sprite = pressedButton;
        buttonPressed = true;
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
        if (!tutorialButtonPress)
        {
            TutorialButtonUsed?.Invoke();
            tutorialButtonPress = true;
        }
        if (collidedObjects.Count == 0)
        {
            buttonPressed = false;
            spriteRenderer.sprite = unpressedButton;
            ButtonSwitchedClosed?.Invoke();
        }
    }
}
