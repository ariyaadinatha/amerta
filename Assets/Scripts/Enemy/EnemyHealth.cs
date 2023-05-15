using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int attackDamage = 10;

    private int currentHealth;
    private bool isDead;

    void Start()
    {
        currentHealth = startingHealth;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("PlayerAttack"))
        {
            currentHealth -= attackDamage;
            Debug.Log("Boss taking damage 1");
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    public void TakeDamage(int amount)
    {
        if (isDead)
            return;

        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;

        // Destroy the enemy object
        Destroy(gameObject, 2f);
    }
}