using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {
    public static SceneController Instance;
    
    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
    }
    
    public void PlayGame() {
        SceneManager.LoadScene("Game");
    }
    
    public void MainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void GameOver() {
        SceneManager.LoadScene("GameOver");
    }

    public void ExitGame() {
        Application.Quit();
    }

    public void GameWon() {
        SceneManager.LoadScene("GameWon");
    }
}
