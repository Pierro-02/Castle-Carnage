using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    [SerializeField] GameObject pausePannel;
    [SerializeField] GameObject gameOverPannel;
    [SerializeField] LayerMask upgradableLayers;

    private bool isGameOver;
    private static bool isGameStarted;

    private void Start() {
        isGameStarted = false;
        isGameOver = false;
    }

    private void FixedUpdate() {
        if (HealthManager.IsGameOver() && !isGameOver) {
            isGameOver = true;
            Time.timeScale = 0f;
            gameOverPannel.SetActive(true);
        }

        if (isGameStarted) {

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

    public static void GameStarted() {
        isGameStarted = true;
    }

    private void TowerUpgrade() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitPoint;

        if (Physics.Raycast(ray, out hitPoint, Mathf.Infinity, upgradableLayers)) {
            //Show Upgrade
        }
    }
}
