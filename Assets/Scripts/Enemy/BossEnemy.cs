using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    public float moveSpeed = 25f;
    public float detectRange = 40f;
    public float idleDuration = 2f;
    public float chargingDuration = 3f;
    public float tailSweepDuration = 2.5f;
    public float shorterIdleDuration = 1f;
    public float shorterChargingDuration = 2f;
    public float shorterTailSweepDuration = 1.5f;
    public int attackDamage = 10;
    public int maxHealth = 100;
    public float halfHealthThreshold = 0.5f;

    private Transform playerTransform;
    private Rigidbody2D rb;
    private float idleTimer;
    private float moveDuration;
    private bool isShorterIdle;
    private int currentHealth;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        currentHealth = maxHealth;
        idleTimer = idleDuration;
        moveDuration = chargingDuration;
        isShorterIdle = false;
    }

    void Update()
    {
        if (currentHealth <= maxHealth * halfHealthThreshold)
        {
            isShorterIdle = true;
        }

        float distance = Vector2.Distance(transform.position, playerTransform.position);

        if (idleTimer > 0f)
        {
            idleTimer -= Time.deltaTime;
            // Perform idle behavior
            // ...
            // Debug.Log("Boss idle");
        }
        else
        {
            if (distance <= detectRange)
            {
                Debug.Log("Boss detect");

                if (moveDuration > 0f)
                {
                    moveDuration -= Time.deltaTime;
                    if (distance <= 10f)
                    {
                        // Perform tail sweep behavior
                        // ...
                        Debug.Log("Boss tail sweep");
                        

                    }
                    else
                    {
                        // Perform charging behavior
                        // ...
                        Debug.Log("Boss charging");
                        Vector2 direction = (playerTransform.position - transform.position).normalized;
                        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
                    }
                }
                else
                {
                    idleTimer = isShorterIdle ? shorterIdleDuration : idleDuration;
                    moveDuration = isShorterIdle ? shorterChargingDuration : chargingDuration;
                }
            }
            else
            {
                idleTimer = isShorterIdle ? shorterIdleDuration : idleDuration;
                moveDuration = isShorterIdle ? shorterChargingDuration : chargingDuration;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
            }
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Perform death behavior
        // ...
    }
}
