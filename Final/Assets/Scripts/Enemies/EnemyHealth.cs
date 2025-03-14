using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {
    [SerializeField] private Slider healthSlider;



    public void UpdateHealthBar(float currentHealth, float maxHealth) {
        healthSlider.value = currentHealth / maxHealth;
    }
    
    
}
