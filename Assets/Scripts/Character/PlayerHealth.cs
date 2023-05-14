using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    private int currentHealth;
    private float barHealth;
    public Image healthBar;

    void Start()
    {
        currentHealth = startingHealth;
    }

    void Update()
    {
        barHealth = (float) currentHealth / startingHealth;
        healthBar.fillAmount = barHealth;
        Debug.Log("barhealth: " + barHealth);
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("Player taking damage: " + currentHealth);
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