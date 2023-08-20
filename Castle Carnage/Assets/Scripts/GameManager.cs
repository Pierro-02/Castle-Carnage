using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    [SerializeField] GameObject pausePannel;

    private void FixedUpdate() {
        if (HealthManager.IsGameOver()) {
            Time.timeScale = 0f;
        }
    }

    public void Pause() {
        pausePannel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume() {
        pausePannel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void MainMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("HomePage");
    }

    public void Restart() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
