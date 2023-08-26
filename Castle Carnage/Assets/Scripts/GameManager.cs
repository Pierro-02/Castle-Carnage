using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    [SerializeField] GameObject pausePannel;
    [SerializeField] GameObject gameOverPannel;
    [SerializeField] GameObject winPannel;
    [SerializeField] EnemySpawner[] waveSpawner;
    [SerializeField] LayerMask upgradableLayers;
    [SerializeField] private TMP_Text _currentWave, maxWave;

    private bool isGameOver;
    private bool isGameWon;
    private static bool isGameStarted;
    private static int currWave;
    private static TMP_Text currentWaveText;
    private float waitForSeconds;

    private void Start() {
        waitForSeconds = 2;
        gameOverPannel.SetActive(false);
        pausePannel.SetActive(false);
        winPannel.SetActive(false);
        currWave = 0;
        currentWaveText = _currentWave;
        maxWave.text = waveSpawner[0].GetMaxWaveIndex().ToString();
        isGameStarted = false;
        isGameOver = false;
        isGameWon = false;
    }

    private void FixedUpdate() {
        if (isGameWon && waitForSeconds <= 0) {
            return;
        }

        if (HealthManager.IsGameOver() && !isGameOver) {
            isGameOver = true;
            Time.timeScale = 0f;
            gameOverPannel.SetActive(true);
        } else if (isGameWon && !isGameOver) {
            waitForSeconds -= Time.deltaTime;
            if (waitForSeconds <= 0) {
                isGameWon = true;
                Time.timeScale = 0f;
                winPannel.SetActive(true);
            }
        }
        CheckWin();
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

    public void NextLevel() {
        int currentLevel = LevelManager.GetCurrentLevelID();
        currentLevel++;
        LevelManager.SetCurrentLevel(currentLevel);
        if (currentLevel == LevelManager.GetLastLevelID()) {
            Debug.Log("No More Levels");
        } else if (currentLevel < 10) {
            string nextLevel = "Level 0" + currentLevel;
            Debug.Log(nextLevel);
            SceneManager.LoadScene(nextLevel);
        }
        Time.timeScale = 1f;
    }

    public void GameStarted() {
        isGameStarted = true;
    }

    public static void UpdateWave(int id = 0) {
        if (id == 0) {
            currWave++;
            currentWaveText.text = currWave.ToString();
        }
    }

    private void CheckWin() {
        bool win = true;
        foreach (var spawner in waveSpawner) {
            if (!spawner.CheckWin()) {
                win = false;
            }
        }
        isGameWon = win;
    }
}
 