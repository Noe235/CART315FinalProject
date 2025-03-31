using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    static public float health;
    public float maxHealth;
    public Image healthBar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = Mathf.Clamp(health/maxHealth, 0, 1);
    }
    
    public void TakeDamage(float damageAmount) {
        health -= damageAmount;
        healthBar.fillAmount = Mathf.Clamp(health/maxHealth, 0, 1);
        Debug.Log(health);
        if (health <= 0) {
            SceneManager.LoadScene("Scenes/Game Scene/GameOverScene");
        }
     
    }
}
