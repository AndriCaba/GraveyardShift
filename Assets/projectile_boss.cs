using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile_boss : MonoBehaviour
{
    public int damage = 10;
    public float lifetime = 2; // Time before the projectile is destroyed
    public float knockbackForce = 5f; // Force of knockback when hitting the player
    public float moveSpeed = 5f; // Speed at which the projectile moves towards the player

    public Transform playerTransform; // Reference to the player's transform
    public GameObject hitEffect; // Reference to the hit effect prefab

    private void Start()
    {
        // Find the player by tag and get its transform
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform; // Assign the player transform
        }

        // Destroy the projectile after a certain amount of time
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        // If the player exists, move the projectile towards the player's position
        if (playerTransform != null)
        {
            Vector2 direction = (playerTransform.position - transform.position).normalized;
           // transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the projectile hits the player
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Projectile hit the player!");

            // Get the player health component and apply damage
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;
                playerHealth.TakeDamage(damage, knockbackDirection);  // Deal damage to the player
                ApplyKnockback(collision.gameObject.GetComponent<Rigidbody2D>(), knockbackDirection);
            }

            // Instantiate the hit effect before destroying the projectile
            if (hitEffect != null)
            {
                Instantiate(hitEffect, transform.position, Quaternion.identity);
            }

            // Destroy the projectile after hitting the player
            Destroy(gameObject);
        }
    }

    private void ApplyKnockback(Rigidbody2D playerRb, Vector2 knockbackDirection)
    {
        if (playerRb != null)
        {
            playerRb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
        }
    }
}