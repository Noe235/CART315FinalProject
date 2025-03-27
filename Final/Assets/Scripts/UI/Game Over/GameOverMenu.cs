using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{

    public void OnTryAgainButtonPressed() {
        // game scene to load
        SceneManager.LoadScene("Scenes/Game Scene/Level1");
    }
}
