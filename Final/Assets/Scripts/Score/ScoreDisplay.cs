using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Score display UI
public class ScoreDisplay : MonoBehaviour {
    // textmeshpro UI
    public TextMeshProUGUI scoreText;

    void Update() {
        if (Score.Score.Instance != null) {
            //  Show the current score 
            scoreText.text = "Score" + Score.Score.Instance.currentScore;
        }
    }
}