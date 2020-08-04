using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Variables
    [SerializeField] private ButtonScript button;
    [SerializeField] private Sprite closedDoor;
    [SerializeField] private Sprite openedDoor;
    SpriteRenderer spriteRenderer;
    Collider2D collider2D;
    [SerializeField] private Door linkingDoor;
    private bool doorLock;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2D = GetComponent<Collider2D>();
    }

   
    void OnEnable()
    {
        collider2D.isTrigger = false;
        button.ButtonSwitchedOpen += Button_ButtonSwitchedOpen;
        button.ButtonSwitchedClosed += Button_ButtonSwitchedClosed;
    }

    void OnDisable()
    {
        button.ButtonSwitchedOpen -= Button_ButtonSwitchedOpen;
        button.ButtonSwitchedClosed -= Button_ButtonSwitchedClosed;
    }

    private void Button_ButtonSwitchedClosed()
    {
        // CLOSE DOOR
        collider2D.isTrigger = false;
        spriteRenderer.sprite = closedDoor;
    }

    private void Button_ButtonSwitchedOpen()
    {
        // OPEN DOOR
        collider2D.isTrigger = true;
        spriteRenderer.sprite = openedDoor;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (spriteRenderer.sprite == openedDoor && !doorLock)
        {
            linkingDoor.doorLock = true;
            doorLock = true;
            col.transform.position = linkingDoor.transform.position - new Vector3(0, 0.5f);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        StartCoroutine(DoorCooldown());
    }

    private IEnumerator DoorCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        linkingDoor.doorLock = false;
        doorLock = false;
    }
}
