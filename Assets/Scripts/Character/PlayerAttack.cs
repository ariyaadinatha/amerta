using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int attackDamage = 50;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Attack();
        }
    }

    void Attack()
    {
        Debug.Log("attacking");
        // animation
        // animator.SetTrigger("AttackTrigger");

        // sound effect
        // audioSource.PlayOneShot(attackSound);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 4f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.tag == "Enemy")
            {
                // Damage the enemy
                EnemyHealth enemyHealth = collider.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    Debug.Log("Enemy hiited");
                    enemyHealth.TakeDamage(attackDamage);
                }
            }
        }
    }
}