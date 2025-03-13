using System;
using UnityEngine;

public class Projectile : MonoBehaviour {
    
    public GameObject impactVFX;
    
    private bool collided;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag != "Player" && other.gameObject.tag != "Bullet" && !collided) {
            collided = true;
            var impact = Instantiate(impactVFX,other.contacts[0].point, Quaternion.identity) as GameObject;
            
            Destroy(impact, 2f);
            Destroy(gameObject);
        }
    }
}
