using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour {
    static public float health;
    public float maxHealth;
    public Image healthBar;
    public GameObject[] fireWands;
    public GameObject[] iceWands;
    public Image FireSpell, IceSpell;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        health = maxHealth;

    }

    // Update is called once per frame
    void Update() {
        healthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);
    }

    public void TakeDamage(float damageAmount) {
        health -= damageAmount;
        healthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);
        Debug.Log(health);
        if (health <= 0) {
            SceneManager.LoadScene("Scenes/Game Scene/GameOverScene");
        }

    }


    public void UpdateWand() {
        // Disable all wands first
        foreach (GameObject wand in fireWands) {
            wand.SetActive(false);
        }

        foreach (GameObject wand in iceWands) {
            wand.SetActive(false);
        }

        if (FPSShooter.spellLevelFire >= 1 && FPSShooter.spellLevelFire <= fireWands.Length && FPSShooter.spell == "Fire") {
            fireWands[FPSShooter.spellLevelFire - 1].SetActive(true);
        }

        if (FPSShooter.spellLevelIce >= 1 && FPSShooter.spellLevelIce <= iceWands.Length && FPSShooter.spell == "Ice") {
            iceWands[FPSShooter.spellLevelIce - 1].SetActive(true);
        }
    }

    public void UpdateSpell() {
        if (FPSShooter.spell == "Fire") {
            SetAlpha(0.5f,IceSpell);
            SetAlpha(1,FireSpell);
        }

        if (FPSShooter.spell == "Ice") {
            SetAlpha(1,IceSpell);
            SetAlpha(0.5f,FireSpell);
        }
    }
    
    public void SetAlpha(float alpha, Image image) {
        Color color = image.color;
        color.a = alpha;
        image.color = color;
    }
}
