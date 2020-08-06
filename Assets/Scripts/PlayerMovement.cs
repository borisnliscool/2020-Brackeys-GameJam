using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public delegate void PlayerMove();
    public event PlayerMove PlayerMoved;
    public event PlayerMove PlayerJumped;

    public Rigidbody2D rb;
    public float JumpSpeed;
    public float movementSpeed;
    public bool isGrounded = false;
    public float smoothSpeed;
    public LayerMask everyLayer;
    private bool tutorialMoved;
    private bool tutorialJumped;
    public bool tutorialFreeze;
    public bool tutorialJumpFreeze;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        if (FreezeTime.i.timeFroze)
        {
            return;
        }
        if (tutorialFreeze)
        {
            return;
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput != 0 && !tutorialMoved)
        {
            tutorialMoved = true;
            StartCoroutine(TutorialMoved());
        }

        rb.velocity = new Vector2(horizontalInput * movementSpeed, rb.velocity.y);

        if (Input.GetKey(KeyCode.Space) && isGrounded && !tutorialJumpFreeze)
        {
            if (!tutorialJumped)
            {
                tutorialJumped = true;
                PlayerMoved?.Invoke();
            }
            isGrounded = false;
            rb.velocity = new Vector2(rb.velocity.x, Time.deltaTime * JumpSpeed);
            PlayerJumped?.Invoke();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.isTrigger)
        {
            return;
        }
        isGrounded = true;
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.isTrigger)
        {
            return;
        }
        isGrounded = true;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        isGrounded = false;

    }

    IEnumerator TutorialMoved()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerMoved?.Invoke();
    }
}
