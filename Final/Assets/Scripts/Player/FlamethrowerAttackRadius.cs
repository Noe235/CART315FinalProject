using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FlamethrowerAttackRadius : MonoBehaviour {

    private Dictionary<string,FollowingEnemy> EnemiesInRadius = new Dictionary<string, FollowingEnemy>();

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

        }
    }
    
    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "BasicEnemy") {
            var collided = other.GetComponent<FollowingEnemy>();
            EnemiesInRadius.Remove(collided.uuid);
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
            nextTick = DateTime.Now.AddMilliseconds(TickInterval);
        }
    }
}
