using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    Vector2 moveInput;
    LayerMask layerMask;
    bool isShoting;
    bool isAlive = true;
    bool endLevel;
    bool playerHasHorizantalSpeed;
    Rigidbody2D rb2d;
    Animator animator;
    ExitLevel exitLevel;
    CircleCollider2D myFeetCollider;
    CapsuleCollider2D myCapsuleCollider;
    GameSession gameSession;

    public GameObject bullet;
    public Transform gun;
    public float moveSpeed = 5f;

    [SerializeField] Vector2 deathBomb;
    [SerializeField] float jumpSpeed = 5f;
    

    // Start is called before the first frame update
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        exitLevel = FindObjectOfType<ExitLevel>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<CircleCollider2D>();
        layerMask = LayerMask.GetMask("Ground");
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive || endLevel) { return; }

        Run();
        Death();
        FlipSprite();
        animator.SetBool("isJumping", !myFeetCollider.IsTouchingLayers(layerMask));
        animator.SetBool("isAlive", isAlive);
        endLevel = exitLevel.hasEnd;
    }

    void OnJump(InputValue value)
    {
        if (!isAlive || endLevel) { return; }

        if (value.isPressed && myFeetCollider.IsTouchingLayers(layerMask))
        {
            rb2d.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    void OnFire(InputValue value)
    {
        if (!isAlive || endLevel) { return; }
        
        //The Shotting system need to be fixed

        isShoting = value.isPressed;
    }

    void Shot()
    {
        //The Shotting system need to be fixed

        if (isShoting)
            Instantiate(bullet, gun.position, transform.rotation);
    }

    void OnMove(InputValue value)
    {
        if (!isAlive || endLevel) { return; }
        moveInput = value.Get<Vector2>();
    }

    void Run()
    {
        if (!isAlive || endLevel) { return; }
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, rb2d.velocity.y);
        rb2d.velocity = playerVelocity;

        animator.SetBool("isRunning", playerHasHorizantalSpeed);
    }

    void FlipSprite()
    {
        if (!isAlive || endLevel) { return; }
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
            gameSession.ProcessPlayerDeath();
        }
    }
}
