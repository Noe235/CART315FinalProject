using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoreHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;
    
    // health bar
    [SerializeField] private EnemyHealth healthBar;

    private void Start()
    {
        currentHealth = maxHealth;
        // assign health bar in inspector
        healthBar = GetComponentInChildren<EnemyHealth>();
    }

    public void TakeDamage(float damage) {
        // TO DO: FIX DEBUG LOG: Debug.Log(gameObject.name + " took " + damage + " damage. Current HP: " + currentHealth);
        Debug.Log($"{gameObject.name} took {damage} damage! Core HP is now: {currentHealth}");
        currentHealth -= damage;
        
        // update floating bar
        if (healthBar != null) {
            healthBar.UpdateHealthBar(currentHealth, maxHealth);
        }
        
        // check for death
        if (currentHealth <= 0) {
            //TO DO FIX DEBUG (it doesn't work) Debug.Log(gameObject.name + " has died!");
            // Debug.Log($"{gameObject.name} has 0 HP! Game Over...");
             
            // unlock cursor and show it (to click button in game over scene)
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            
            // load game over scene
            SceneManager.LoadScene("Scenes/Game Scene/GameOverScene");
            
            Destroy(gameObject);
        }
    }
}
