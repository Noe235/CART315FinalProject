using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerZone : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        // Check if the object is one of your FallingSpheres
        if (other.CompareTag("FallingSphere")) {
            // Use the Singleton Spawner to remove and destroy the sphere
            if (Spawner.instance != null) {
                Spawner.instance.RemoveSphere(other.gameObject); 
            }
            else {
                // if no spawner,  destroy object 
                Destroy(other.gameObject);
            }
        }
    }
}