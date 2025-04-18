using UnityEngine;
using UnityEngine.AI;

public class FollowingEnemy : MonoBehaviour
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
    
    
    public enum TargetType {
        CoreOnly,
        PlayerOnly 
    }

    void Awake() {
        uuid = System.Guid.NewGuid().ToString();
    }
   
    void Start() {
        health = maxHealth;
        core = GameObject.FindGameObjectWithTag("Core");
        player = GameObject.FindGameObjectWithTag("Player");
        playerManager = GameObject.FindGameObjectWithTag("PlayerManager");
        
        
        
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
        if (targetType == TargetType.CoreOnly && other.gameObject.CompareTag("Core")) {
            CoreHealth ch = other.gameObject.GetComponent<CoreHealth>();
            if (ch != null) {
                ch.TakeDamage(enemyDamage);
                Debug.Log(name + " collided with Core, dealing " + enemyDamage + " damage.");
            }
        }
        
        
   }

   
   //have to use this one because the player has the controller
   private void OnTriggerEnter(Collider other) {
       if (targetType == TargetType.PlayerOnly && other.gameObject.CompareTag("Player")) {
           playerManager.GetComponent<PlayerManager>().TakeDamage(enemyDamage);
           Debug.Log(name + " collided with Player, dealing " + enemyDamage + " damage. Player HP is now: " + PlayerManager.health);
       }
   }

   public bool TakeDamage(float damageAmount) {
        health -= damageAmount;
        if (healthBar) {
            healthBar.UpdateHealthBar(health, maxHealth);
        }
        if (health <= 0) {
            EnemySpawner.enemiesAlive--;
            Debug.Log("Killed enemy, " + EnemySpawner.enemiesAlive + " remaining");
            Destroy(gameObject);
            return true;
        }
        return false;
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

