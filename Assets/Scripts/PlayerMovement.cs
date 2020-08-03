using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float JumpSpeed;
    public float movementSpeed;
    public bool isGrounded = false;
    private Vector2 startPos = new Vector2(0, 0);
    public float smoothSpeed;
    private CircleCollider2D circle;
    public LayerMask everyLayer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        circle = GetComponent<CircleCollider2D>();
    }

    public void TpToStart()
    {
        transform.position = startPos;
    }

    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(horizontalInput * movementSpeed, rb.velocity.y);

        if (Input.GetKey(KeyCode.Space) && isGrounded == true)
        {
            isGrounded = false;
            rb.AddForce(transform.up * JumpSpeed);
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {

        isGrounded = true; 
    }

    

    void OnTriggerExit2D(Collider2D col)
    {

        isGrounded = false;

    }
}
