using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Camera cam;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;


    void Awake() {
        cam = Camera.main;
    }
    public void UpdateHealthBar(float currentHealth, float maxHealth) {
        healthSlider.value = currentHealth / maxHealth;
    }

    void Update() {
        transform.rotation = cam.transform.rotation;
        transform.position = target.position + offset;
    }
}
