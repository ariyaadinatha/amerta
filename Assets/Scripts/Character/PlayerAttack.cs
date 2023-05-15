using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int attackDamage = 20;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

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
        // Perform attack logic here
        // Play attack animation
        // animator.SetTrigger("AttackTrigger");
        anim.SetTrigger("Attack");

        // Play attack sound effect
        // audioSource.PlayOneShot(attackSound);
    }

    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if (collision.CompareTag("Enemy"))
    //     {
    //         EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
    //         if (enemyHealth != null)
    //         {
    //             enemyHealth.TakeDamage(attackDamage);
    //         }
    //     }
    // }

    // void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (collision.collider.CompareTag("Enemy"))
    //     {
    //         EnemyHealth enemyHealth = collision.collider.GetComponent<EnemyHealth>();
    //         if (enemyHealth != null)
    //         {
    //             enemyHealth.TakeDamage(attackDamage);
    //             Debug.Log("Enemy taking damage...");
    //         }
    //     }
    // }
}
