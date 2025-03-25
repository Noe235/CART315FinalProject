using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    [Header("Health Settings")]
    public float maxHealth = 100f;

    private float currentHealth;

    // Used with HealthBar.cs: to notify when damage occurs
    public delegate void HealthChangedDelegate(float current, float max);
    public event HealthChangedDelegate OnHealthChanged;

    void Start() {
        // Initialize current health
        currentHealth = maxHealth;
    }

    public float GetCurrentHealth() {
        return currentHealth;
    }

    // Combined TakeDamage method
    public void TakeDamage(float damageAmount) {
        currentHealth -= damageAmount;

        // Log for debugging
        Debug.Log(gameObject.name + " took " + damageAmount + " damage. Current HP: " + currentHealth);

        // Notify health bar (or other listeners) via event
        if (OnHealthChanged != null) {
            OnHealthChanged.Invoke(currentHealth, maxHealth);
        }

        // Check for death
        if (currentHealth <= 0) {
            Die();
        }
    }

    void Die() {
        Debug.Log(gameObject.name + " has died!");
        // For now, just destroy the GameObject
        Destroy(gameObject);
    }
}