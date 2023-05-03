using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    private int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int amount)
    {
        Debug.Log("Damage taken");
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            // The player has died
            Die();
        }
    }

    void Die()
    {
        // TODO: Handle the player's death
        Debug.Log("Player has died!");
    }
}