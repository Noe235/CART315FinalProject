using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FlamethrowerAttackRadius : MonoBehaviour {

    private Dictionary<string,FollowingEnemy> EnemiesInRadius = new Dictionary<string, FollowingEnemy>();
    private Dictionary<string, Ghost_Enemy> GhostsInRadius = new Dictionary<string, Ghost_Enemy>();
    [SerializeField] private float damage;
    
    
    
    //invul time
    public DateTime nextTick = DateTime.Now;
    public const int TickInterval = 200;
    
    private void OnTriggerEnter(Collider other) {
        
        if (other.gameObject.tag == "BasicEnemy") {
            
         var collided = other.GetComponent<FollowingEnemy>();
         if (!EnemiesInRadius.ContainsKey(collided.uuid)) {

             EnemiesInRadius.Add(collided.uuid, collided);
         }
         
         // add a new pair for Ghost_Enemy
         var ghosted = other.GetComponent<Ghost_Enemy>();
         if (!GhostsInRadius.ContainsKey(ghosted.uuid)) {
             GhostsInRadius.Add(ghosted.uuid, ghosted);
         }

        }
    }
    
    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "BasicEnemy") {
            var collided = other.GetComponent<FollowingEnemy>();
            EnemiesInRadius.Remove(collided.uuid);
            
            // add Ghost_Enemy script
            var ghosted = other.GetComponent<Ghost_Enemy>();
            GhostsInRadius.Remove(ghosted.uuid);
        }
    }
    private void OnDisable() {
        EnemiesInRadius.Clear();
    }
    private void Update() {
        Flamed();
    }

    private void Flamed() {
        if (DateTime.Now > nextTick) {
            List<string> toRemove = new List<string>();
            foreach (var enemy in EnemiesInRadius.Values) {
                if (enemy.TakeDamage(damage)) {
                    toRemove.Add(enemy.uuid);
                }
            }
            foreach (string uuid in toRemove) {
                EnemiesInRadius.Remove(uuid);
            }
            
            // also damage all Ghost_Enemy
            List<string> ghostRemove = new List<string>();
            foreach (var ghost in GhostsInRadius.Values) {
                if (ghost.TakeDamage(damage)) {
                    ghostRemove.Add(ghost.uuid);
                }
            }
            foreach (string gID in ghostRemove) {
                GhostsInRadius.Remove(gID);
            }
            
            nextTick = DateTime.Now.AddMilliseconds(TickInterval);
        }
    }
}
