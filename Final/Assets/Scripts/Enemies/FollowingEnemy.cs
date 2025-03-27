using System;
using System.Collections;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.AI;

public class FollowingEnemy : MonoBehaviour, IBurnable //IDamageable
{
    [SerializeField] private GameObject core;
    [SerializeField] private GameObject player;
    [SerializeField] private float followspeed;

    [SerializeField] private float health;
    [SerializeField] private float maxHealth;
    [SerializeField] private EnemyHealth healthBar;
    [SerializeField] private float enemyDamage = 10;
    
    [SerializeField] private TargetType targetType = TargetType.CoreOnly;

    public NavMeshAgent agent;

    // from flame thrower tutorial proably can refine some stuff
    [SerializeField] private int _Health;
    public int Health{get => _Health; set => _Health = value; }
    
    [SerializeField]
    private bool _IsBurning;
    public bool IsBurning { get => _IsBurning; set => _IsBurning = value; }
    
    private Coroutine BurnCoroutine;
    
    public event DeathEvent OnDeath;
    public delegate void DeathEvent(Enemy Enemy);
    
    public enum TargetType {
        CoreOnly,
        PlayerOnly 
    }
    
   
    void Start() {
        health = maxHealth;
        core = GameObject.FindGameObjectWithTag("Core");
        player = GameObject.FindGameObjectWithTag("Player");
        
        if (!healthBar) {
            healthBar = GetComponentInChildren<EnemyHealth>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // switch (state) {
        //     default:
        //         case State.HeadToCore:
        //             //Previous movement toward target script
        //            // transform.position = Vector3.MoveTowards(transform.position, target.transform.position, followspeed * Time.deltaTime); //go toward target
        //           //  transform.forward = target.transform.position - transform.position;
        //            agent.SetDestination(core.transform.position); //to work nav mesh surface needs to be put on the surface
        //             FindPlayer();
        //         break;
        //     
        //     case State.AttackPlayer:
        //         agent.SetDestination(player.transform.position);
        //
        //         float attackRange = 10f;
        //         
        //         //target too far go back to core
        //         if (Vector3.Distance(transform.position, player.transform.position) > 18f) {
        //             state = State.HeadToCore;
        //         }
        //         if (Vector3.Distance(transform.position, player.transform.position) < attackRange) {
        //             //within attack range
        //             //in prevention of the attaack being on each frames 
        //            // if (Time.time > nextshootTime){
        //            //float firerate = enemy.attackspeed
        //            //nexshootTime = Time.time + firerate
        //            // }
        //            //
        //             //insert attack animation or like script
        //         }

        // updated to move toward only the assigned target (core or player)
        switch (targetType) {
            case TargetType.CoreOnly:
                if (core != null) {
                    agent.SetDestination(core.transform.position);
                }
                break;

            case TargetType.PlayerOnly:
                if (player != null) {
                    agent.SetDestination(player.transform.position);
                }
                break;
        }
    }

   private void OnCollisionEnter(Collision other) {
        // if (other.gameObject.tag == "Player") {
        //     // damage player
        //     PlayerManager.health -= enemyDamage; 
       
        
        // If this enemy is PlayerOnly, damage the player
        if (targetType == TargetType.PlayerOnly && other.gameObject.CompareTag("Player")) {
            // Decrease static player HP
            PlayerManager.health -= enemyDamage;
            Debug.Log(name + " collided with Player, dealing " + enemyDamage + " damage. Player HP is now: " + PlayerManager.health);

            // If this enemy is CoreOnly, damage the core
        } else if (targetType == TargetType.CoreOnly && other.gameObject.CompareTag("Core")) {
            CoreHealth ch = other.gameObject.GetComponent<CoreHealth>();
            if (ch != null) {
                ch.TakeDamage(enemyDamage);
                Debug.Log(name + " collided with Core, dealing " + enemyDamage + " damage.");
            }
        }
        
        
   }

    public void TakeDamage(float damageAmount) {
        health -= damageAmount;
        if (healthBar) {
            healthBar.UpdateHealthBar(health, maxHealth);
        }
        if (health <= 0) {
            StopBurning();
            Destroy(gameObject);
        }
    }

  

    public void StartBurning(int DamagePerSecond) {
        IsBurning = true;
        if (BurnCoroutine != null) {
            StopCoroutine(BurnCoroutine);
        }

        BurnCoroutine = StartCoroutine(Burn(DamagePerSecond));
    }

    private IEnumerator Burn(int DamagePerSecond) {
        float minTimeToDamage = 1f/DamagePerSecond;
        WaitForSeconds wait = new WaitForSeconds(minTimeToDamage);
        int damagePerTick = Mathf.FloorToInt(minTimeToDamage) + 1;
        
        TakeDamage(damagePerTick);
        while (IsBurning) {
            yield return wait;
            TakeDamage(damagePerTick);
        }
    }

    public void StopBurning() {
        IsBurning = false;
        if (BurnCoroutine != null) {
            StopCoroutine(BurnCoroutine);
        }
    }
    
    
    // private void FindPlayer() {
    //     float playerDetectRange = 15f;
    //     player = GameObject.FindGameObjectWithTag("Player");
    //     if (Vector3.Distance(transform.position, player.transform.position) < playerDetectRange) {
    //         //if near the player range change state
    //         state = State.AttackPlayer;
    //     }
    // }
}

public interface IDamageable {
    public int Health { get; set; }
    public void TakeDamage(int damageAmount);
}


public interface IBurnable {
    public bool IsBurning { get; set; }
    public void StartBurning(int DamagePerSecond);
    public void StopBurning();
}
