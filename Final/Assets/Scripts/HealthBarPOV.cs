using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attatch to HealthBarCanvas
public class HealthBarPOV : MonoBehaviour {
    public Transform target;       
    public float verticalOffset = 2f; // How high above the target pivot to place the bar

    void Update() {
        if (target == null) return;
        
        // Position: place the bar above the target
        Vector3 newPos = target.position + Vector3.up * verticalOffset;
        transform.position = newPos;
        
        if (Camera.main != null) {
            // make health bar face camera
            transform.LookAt(transform.position + Camera.main.transform.forward);
        }
    }
}