using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D rb2d;
    Animator animator;
    bool playerHasHorizantalSpeed;
    LayerMask layerMask;
    CapsuleCollider2D collider;

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<CapsuleCollider2D>();
        layerMask = LayerMask.GetMask("Ground");
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        FlipSprite();
        animator.SetBool("isJumping", !collider.IsTouchingLayers(layerMask));
    }

    void OnJump(InputValue value)
    {
        if (value.isPressed && collider.IsTouchingLayers(layerMask))
        {
            rb2d.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, rb2d.velocity.y);
        rb2d.velocity = playerVelocity;

        animator.SetBool("isRunning", playerHasHorizantalSpeed);
    }

    void FlipSprite()
    {
        playerHasHorizantalSpeed = Mathf.Abs(rb2d.velocity.x) > Mathf.Epsilon;

        if(playerHasHorizantalSpeed)
            transform.localScale = new Vector2(Mathf.Sign(rb2d.velocity.x) * Mathf.Abs(transform.localScale.x), transform.localScale.y);
    }
}
