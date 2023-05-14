using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public float moveSpeed = 35f;
    public float detectRange = 50f;
    public float idleDuration = 4f;

    public float shorterIdleDuration = 2f;

    public int attackDamage = 10;
    public int maxHealth = 100;
    public float halfHealthThreshold = 0.5f;

    private Vector2 chargeTarget;

    private Transform playerTransform;
    private Rigidbody2D rb;
    private float idleTimer;
    private float moveDuration;
    private bool isShorterIdle;
    private int currentHealth;

    private Vector2 lastKnownPosition;
    [SerializeField] private bool isCharging;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        currentHealth = maxHealth;
        idleTimer = idleDuration;
        // moveDuration = chargingDuration;
        isShorterIdle = false;
    }

    void Update()
    {
        if (currentHealth <= maxHealth * halfHealthThreshold)
        {
            isShorterIdle = true;
        }

        float distance = Vector2.Distance(transform.position, playerTransform.position);
        

        if (isCharging == true)
        {
            // Perform charging behavior
            Vector2 direction = (chargeTarget - rb.position).normalized;
            Debug.Log("charging");
            rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);
            
            // berhenti setelah
            if (Vector2.Distance(chargeTarget, rb.position) < 10f)
            {
                isCharging = false;
                idleTimer = idleDuration;
                Debug.Log("idle reset 2");
            }
        }
        else
        {
            if (idleTimer > 0f)
            {
                // Perform idle behavior
                idleTimer -= Time.deltaTime;
            }
            else
            {
                // detect player
                if (distance <= detectRange)
                {
                
                    if (distance <= 15f)
                    {
                        // Perform tail sweep behavior
                        Debug.Log("Tail sweep");
                        idleTimer = idleDuration;
                        Debug.Log("idle reset 3");
                    }
                    else
                    {
                        isCharging = true;
                        chargeTarget = playerTransform.position;
                        chargeTarget.y = rb.position.y;
                    }                
                }
                else
                {
                    idleTimer = idleDuration;
                    Debug.Log("idle reset");
                }
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.collider.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
                Debug.Log("Hit..");
            }
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("Boss taking damage");
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
