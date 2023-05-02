using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float detectRange = 10f;
    public int damageAmount = 10;

    private Transform playerTransform;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, playerTransform.position);

        if (distance <= detectRange)
        {
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
            Debug.Log("detected");


            // Check if the enemy is touching the player
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 4f);
            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject.tag == "Player")
                {
                    // Damage the player
                    Debug.Log("Damage!");
                    // collider.gameObject.GetComponent<PlayerHealth>().TakeDamage(damageAmount);
                }
            }
        }
    }
}



            