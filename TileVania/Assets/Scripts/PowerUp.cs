using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    Movement playerMovment;
    SpriteRenderer spriteRenderer;

    [SerializeField] Color32 playerColor;

    // Start is called before the first frame update
    void Start()
    {
        playerMovment = FindObjectOfType<Movement>();
        spriteRenderer = FindObjectOfType<Movement>().GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Destroy(gameObject);
            playerMovment.moveSpeed += 2;
            spriteRenderer.color = playerColor;
        }
    }
}
