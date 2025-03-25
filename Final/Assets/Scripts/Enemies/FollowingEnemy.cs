using System;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.AI;

public class FollowingEnemy : MonoBehaviour
{
    [SerializeField] private GameObject core;
    [SerializeField] private GameObject player;
    [SerializeField] private float followspeed;

    [SerializeField] private float health;
    [SerializeField] private float maxHealth;
    [SerializeField] private EnemyHealth healthBar;
    [SerializeField] private float enemyDamage = 10;

    public NavMeshAgent agent;
    
    private enum State {
        HeadToCore,
        AttackPlayer,
    }
    private State state;

    void Awake() {
        healthBar = GetComponentInChildren<EnemyHealth>();
        state = State.HeadToCore;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    { 
        health = maxHealth;
      core = GameObject.FindGameObjectWithTag("Core");
      player = GameObject.FindGameObjectWithTag("Player");
      
      
    }

    // Update is called once per frame
    void Update()
    {
        switch (state) {
            default:
                case State.HeadToCore:
                    //Previous movement toward target script
                   // transform.position = Vector3.MoveTowards(transform.position, target.transform.position, followspeed * Time.deltaTime); //go toward target
                  //  transform.forward = target.transform.position - transform.position;
                   agent.SetDestination(core.transform.position); //to work nav mesh surface needs to be put on the surface
                    FindPlayer();
                break;
            
            case State.AttackPlayer:
                agent.SetDestination(player.transform.position);

                float attackRange = 10f;
                
                //target too far go back to core
                if (Vector3.Distance(transform.position, player.transform.position) > 18f) {
                    state = State.HeadToCore;
                }
                if (Vector3.Distance(transform.position, player.transform.position) < attackRange) {
                    //within attack range
                    //in prevention of the attaack being on each frames 
                   // if (Time.time > nextshootTime){
                   //float firerate = enemy.attackspeed
                   //nexshootTime = Time.time + firerate
                   // }
                   //
                    //insert attack animation or like script
                }

                break;
        }
      
        
    }

   private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Player") {
            PlayerManager.health -= enemyDamage;
        }
    }

    public void TakeDamage(float damageAmount) {
        health -= damageAmount;
        healthBar.UpdateHealthBar(health,maxHealth);
        if (health <= 0) {
            Destroy(gameObject);
        }
    }

    private void FindPlayer() {
        float playerDetectRange = 15f;
        player = GameObject.FindGameObjectWithTag("Player");
        if (Vector3.Distance(transform.position, player.transform.position) < playerDetectRange) {
            //if near the player range change state
            state = State.AttackPlayer;
        }
    }
}
