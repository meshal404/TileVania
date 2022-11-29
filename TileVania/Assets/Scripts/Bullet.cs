using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float xSpeed;
    Rigidbody2D rb;
    Movement player;
    CircleCollider2D circle2d;

    [SerializeField] float bulletSpeed;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Movement>();
        rb = GetComponent<Rigidbody2D>();
        circle2d = GetComponent<CircleCollider2D>();

        xSpeed = bulletSpeed * Mathf.Sign(player.transform.localScale.x);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(xSpeed, 0f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (circle2d.IsTouchingLayers(LayerMask.GetMask("Enemies")))
            Destroy(collision.gameObject);

        Destroy(gameObject);
    }
}
