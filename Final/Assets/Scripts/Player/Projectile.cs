using System;
using UnityEngine;

public class Projectile : MonoBehaviour {
    
    public GameObject impactVFX;
    public float damage = 10;
 
    
    private bool collided;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        if (FPSShooter.spellLevelFire == 2) {
            damage = damage*2;
        }
    }
    
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag != "Player" && other.gameObject.tag != "Bullet" && !collided) {
            collided = true;
            var impact = Instantiate(impactVFX,other.contacts[0].point, Quaternion.identity) as GameObject;
            
            Destroy(impact, 2f);
            if (other.gameObject.tag == "BasicEnemy") {
                other.gameObject.GetComponent<FollowingEnemy>().TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
