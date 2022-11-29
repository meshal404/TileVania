using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float enemySpeed = 1f;

    Rigidbody2D enemyRb;
    BoxCollider2D enemyBoxCollider;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        enemyBoxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyRb.velocity = new Vector2(enemySpeed, enemyRb.velocity.y);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        enemySpeed = -enemySpeed;
        transform.localScale = new Vector2(Mathf.Sign(enemySpeed) * Mathf.Abs(transform.localScale.x), transform.localScale.y);
    }
}
