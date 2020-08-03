using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Variables
    Button button;
    [SerializeField] private Sprite closedDoor;
    [SerializeField] private Sprite openedDoor;
    SpriteRenderer spriteRenderer;
    [SerializeField] private Door linkingDoor;
    public bool doorLock;
    void Awake()
    {
        button = FindObjectOfType<Button>(); // BETTER WAY TO LINK THIS, FIX LATER
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

   
    void OnEnable()
    {
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
        spriteRenderer.sprite = closedDoor;
    }

    private void Button_ButtonSwitchedOpen()
    {
        // OPEN DOOR
        spriteRenderer.sprite = openedDoor;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (spriteRenderer.sprite == openedDoor && !doorLock)
        {
            linkingDoor.doorLock = true;
            doorLock = true;
            col.transform.position = linkingDoor.transform.position - new Vector3(0, 0.5f);
            Debug.Log("Teleported " + col.name);
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
