using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour {
    [Header("Movement Settings")] 
    public float moveSpeed = 2f;
    public Transform target; // Assign target (the object to defend) in the Inspector
    
    [Header("Attack Settings")]
    public float attackRange = 1.5f;    // How close the enemy must be to attack.
    public float attackDamage = 10f;    // How much damage each attack deals.
    public float attackCooldown = 1f;   // Seconds between consecutive attacks.
    private float lastAttackTime = 0f;  // Tracks when the last attack occurred.
    
    void Update() {
        if (target != null) {
            MoveTowardsTarget();
            TryAttackTarget();
        }
    }
    
    void MoveTowardsTarget() {
        // Calculate the direction from Enemy to Target
        Vector3 direction = (target.position - transform.position).normalized;
        // Move in that direction at moveSpeed
        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
        // make the enemy face the target (rotates the enemy to look at the target's position on the XZ plane)
        Vector3 lookDirection = new Vector3(target.position.x, transform.position.y, target.position.z);
        transform.LookAt(lookDirection);
    }

    
    /* checks whether the enemy is close enough to attack (within attackRange),
     * and if enough time has passed since last attack (cooldown)
     * If those conditions are met, call AttackTarget().
     */
    void TryAttackTarget() {
        // Check distance between enemy and target
        float distance = Vector3.Distance(transform.position, target.position);
        // If close enough and cooldown has passed
        if (distance <= attackRange && Time.time >= lastAttackTime + attackCooldown) {
            // Perform the attack
            AttackTarget();
            // Reset the lastAttackTime to the current time
            lastAttackTime = Time.time;
        }
    }

    /*
     * When attacking, log a message and then fetch the Health component on the target,
     * if it exists. If found, we call TakeDamage() on the target's Health script, passing in
     * the enemy's attackDamage
     * If the target's health drops to 0 or below, the Health script handles its destruction.
     */
    void AttackTarget() {
        // Log which enemy attacked which target
        Debug.Log(gameObject.name + " attacks " + target.name + " for " + attackDamage + " damage!");

        // Get the Health component on the target, if it exists
        Health targetHealth = target.GetComponent<Health>();
        if (targetHealth != null) {
            // Deal damage to the target
            targetHealth.TakeDamage(attackDamage);
        }
    }
}
