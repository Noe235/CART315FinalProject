using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//score display

public class ScoreDisplay : MonoBehaviour {
    public TextMeshProUGUI scoreText;

    void Update() {
        
        //  Show the current score (Spheres player collides with [from the Spawner])
        scoreText.text = "Score: " + Spawner.instance.score;
    }
}