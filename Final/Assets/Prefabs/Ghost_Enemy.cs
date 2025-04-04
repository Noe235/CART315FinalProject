using System;
using UnityEngine;
using UnityEngine.AI;

public class Ghost_Enemy : MonoBehaviour
{
    [SerializeField] private GameObject core;
    [SerializeField] private GameObject player;
    [SerializeField] private float followspeed;
    [SerializeField] private GameObject playerManager;

    [SerializeField] private float health;
    [SerializeField] private float maxHealth;
    [SerializeField] private EnemyHealth healthBar;
    [SerializeField] private float enemyDamage = 10;

    [SerializeField] private TargetType targetType = TargetType.CoreOnly;
    public NavMeshAgent agent;

    public string uuid;

    public enum TargetType
    {
        CoreOnly,
        PlayerOnly
    }

    void Awake()
    {
        uuid = Guid.NewGuid().ToString();
    }

    void Start()
    {
        health = maxHealth;
        core = GameObject.FindGameObjectWithTag("Core");
        player = GameObject.FindGameObjectWithTag("Player");
        playerManager = GameObject.FindGameObjectWithTag("PlayerManager");

        if (!healthBar) {
            healthBar = GetComponentInChildren<EnemyHealth>();
        }

        if (!agent) {
            agent = GetComponent<NavMeshAgent>();
        }

        if (agent && followspeed > 0) {
            agent.speed = followspeed;
        }
    }

    // -----------------------------------------------------------------------
    // UPDATE
    // -----------------------------------------------------------------------
    void Update()
    {
        // Same logic as original: chase core or player
        switch (targetType) {
            case TargetType.CoreOnly:
                if (core != null && agent) {
                    agent.SetDestination(core.transform.position);
                }

                break;

            case TargetType.PlayerOnly:
                if (player != null && agent) {
                    agent.SetDestination(player.transform.position);
                }

                break;
        }
    }

    // -----------------------------------------------------------------------
    // Collision
    // -----------------------------------------------------------------------
    private void OnCollisionEnter(Collision other)
    {
        if (targetType == TargetType.CoreOnly && other.gameObject.CompareTag("Core")) {
            CoreHealth ch = other.gameObject.GetComponent<CoreHealth>();
            if (ch != null) {
                ch.TakeDamage(enemyDamage);
                Debug.Log(name + " collided with Core, dealing " + enemyDamage + " damage.");

                // TO DO add attack functions:
                // if (anim) anim.CrossFade(AttackState, 0.1f, 0, 0);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (targetType == TargetType.PlayerOnly && other.gameObject.CompareTag("Player")) {
            if (playerManager) {
                playerManager.GetComponent<PlayerManager>().TakeDamage(enemyDamage);
                Debug.Log(name + " collided with Player, dealing " + enemyDamage +
                          " damage. Player HP is now: " + PlayerManager.health);

                // TO DO: Attack animation
                // if (anim) anim.CrossFade(AttackState, 0.1f, 0, 0);
            }
        }
    }

    // -----------------------------------------------------------------------
    // Taking Damage
    // -----------------------------------------------------------------------
    public bool TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        if (healthBar) {
            healthBar.UpdateHealthBar(health, maxHealth);
        }

        if (health <= 0) {
            EnemySpawner.enemiesAlive--;
            Debug.Log("Killed enemy, " + EnemySpawner.enemiesAlive + " remaining");

            // TO DO: Add dissolve animation maybe
            Destroy(gameObject);
            return true;
        }

        return false;
    }
}