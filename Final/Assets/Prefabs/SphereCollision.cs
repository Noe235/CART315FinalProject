using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCollision : MonoBehaviour
{

    // If the ball collides with the player (it is a trigger)
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            Spawner.instance.RemoveSphere(gameObject);
        }
    }
}
