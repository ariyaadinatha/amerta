using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    private int currentHealth;

    void Start()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            // The player has died
            Die();
        }
    }

    public void Heal(int amount)
    {
        Debug.Log("Player healed for " + amount);
        currentHealth += amount;
        if (currentHealth <= 100)
        {
            currentHealth = 100;
        }
    }

    void Die()
    {
        // TODO: Handle the player's death
        Debug.Log("Player has died!");
    }
}