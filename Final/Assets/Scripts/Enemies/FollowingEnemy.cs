using System;
using UnityEngine;

public class FollowingEnemy : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private float followspeed;

    [SerializeField] private float health;
    [SerializeField] private float maxHealth;
    [SerializeField] private EnemyHealth healthBar;


    void Awake() {
        healthBar = GetComponentInChildren<EnemyHealth>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Core");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, followspeed * Time.deltaTime);
        transform.forward = target.transform.position - transform.position;
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Bullet") {
            var damage = GetComponent<Projectile>().damage;
            TakeDamage(damage);
        }
        
        
    }

    private void TakeDamage(float damageAmount) {
        health -= damageAmount;
        healthBar.UpdateHealthBar(health,maxHealth);
        if (health <= 0) {
            Destroy(gameObject);
        }
    }
}
