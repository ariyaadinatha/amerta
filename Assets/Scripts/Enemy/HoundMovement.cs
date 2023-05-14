using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoundMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float detectRange = 10f;
    public int damageAmount = 10;

    private Transform playerTransform;
    private Rigidbody2D rb;
    private bool stunned;
    private float cooldown;
    const float stunnedTime = 6f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, playerTransform.position);

        if (!stunned)
        {
            if (distance <= detectRange)
            {
                Vector2 direction = (playerTransform.position - transform.position).normalized;
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
                /*Debug.Log("detected");*/


                // Check if the enemy is touching the player
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 4f);
                foreach (Collider2D collider in colliders)
                {
                    if (collider.gameObject.tag == "Player")
                    {
                        // Damage the player
                        /*Debug.Log("Damage!");*/
                        // collider.gameObject.GetComponent<PlayerHealth>().TakeDamage(damageAmount);
                    }
                }
            }
        }
    }
    
    private void LateUpdate()
    {
        if (stunned)
        {
            cooldown += Time.deltaTime;
        }
        if(cooldown >= stunnedTime)
        {
            stunned = false;
            cooldown = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Stun")
        {
            /*Debug.Log("Kestun bro");*/
            stunned = true;
        }
    }
}



            