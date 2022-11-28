using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    Rigidbody2D rb2d;
    Animator animator;
    Vector2 moveInput;
    LayerMask layerMask;
    bool playerHasHorizantalSpeed;
    CircleCollider2D myFeetCollider;
    CapsuleCollider2D myCapsuleCollider;

    bool isAlive = true;

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] Vector2 deathBomb;

    // Start is called before the first frame update
    void Start()
    {
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<CircleCollider2D>();
        layerMask = LayerMask.GetMask("Ground");
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) { return; }
        Run();
        Death();
        FlipSprite();
        animator.SetBool("isJumping", !myFeetCollider.IsTouchingLayers(layerMask));
        animator.SetBool("isAlive", isAlive);
    }

    void OnJump(InputValue value)
    {
        if (!isAlive) { return; }

        if (value.isPressed && myFeetCollider.IsTouchingLayers(layerMask))
        {
            rb2d.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    void OnMove(InputValue value)
    {
        if (!isAlive) { return; }
        moveInput = value.Get<Vector2>();
    }

    void Run()
    {
        if (!isAlive) { return; }
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, rb2d.velocity.y);
        rb2d.velocity = playerVelocity;

        animator.SetBool("isRunning", playerHasHorizantalSpeed);
    }

    void FlipSprite()
    {
        if (!isAlive) { return; }
        playerHasHorizantalSpeed = Mathf.Abs(rb2d.velocity.x) > Mathf.Epsilon;

        if(playerHasHorizantalSpeed)
            transform.localScale = new Vector2(Mathf.Sign(rb2d.velocity.x) * Mathf.Abs(transform.localScale.x), transform.localScale.y);
    }

    void Death()
    {
        if (myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Water")))
        {
            isAlive = false;
            rb2d.AddForce(new Vector2(deathBomb.x * -(Mathf.Sign(rb2d.velocity.x)), deathBomb.y), ForceMode2D.Impulse);
        }
            
    }
}
