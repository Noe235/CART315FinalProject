using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    public Health health;    
    public Image fillImage;

    void Start() {
        if (health != null) {
            // Subscribe to the OnHealthChanged event
            health.OnHealthChanged += HandleHealthChanged;
        }
    }

    void Update()
    {
        if (health != null && fillImage != null)
        {
            // Calculate fill amount (from 0 to 1)
            float fillValue = health.GetCurrentHealth() / health.maxHealth;
            fillImage.fillAmount = fillValue;
        }
    }

    void HandleHealthChanged(float current, float max) {
        if (fillImage != null) {
            fillImage.fillAmount = current / max;
        }
    }
}
