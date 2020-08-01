using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float JumpSpeed;
    public float movementSpeed;
    private bool isGrounded = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        transform.position = transform.position + new Vector3(horizontalInput * movementSpeed * Time.deltaTime, 0, 0);


        if (Input.GetKey(KeyCode.Space) && isGrounded == true)
        {
            isGrounded = false;
            rb.AddForce(transform.up * JumpSpeed);
        }

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        isGrounded = true;
    }

    void OnCollisionExit2D(Collision2D col)
    {
        isGrounded = false;
    }
}
