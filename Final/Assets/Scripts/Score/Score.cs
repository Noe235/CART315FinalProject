using UnityEngine;

// Score Singleton

namespace Score
{
    public class Score : MonoBehaviour {
        // singleton
        public static Score Instance;
        // player current score
        public int currentScore = 0;

        void Awake() {
            if (Instance == null) {
                Instance = this;
            } else {
                Destroy(gameObject);
            }
        }

        // add points
        public void AddScore(int points) {
            currentScore += points;
            Debug.Log("Score: " + currentScore);
        }
    }
}
