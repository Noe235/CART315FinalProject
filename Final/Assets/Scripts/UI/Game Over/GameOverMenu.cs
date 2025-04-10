using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        // get the final score from the last run
        int finalScore = EnemySpawner.currentRunScore;
        
        // compare to stored HighScore
        int best = PlayerPrefs.GetInt("HighScore", 0); // default 0
        if (finalScore > best) {
            best = finalScore;
            PlayerPrefs.SetInt("HighScore", best);
        }
        PlayerPrefs.Save(); 

        // update the UI text fields
        if (finalScoreText) {
            finalScoreText.text = "Score: " + finalScore;
        }
        if (highScoreText) {
            highScoreText.text = "High Score: " + best;
        }
    }
    
    public void OnTryAgainButtonPressed() {
        // game scene to load
        SceneManager.LoadScene("Scenes/Game Scene/Level1");
    }
}
