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
    private static bool isGameWon;
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
            SoundSystem.PlayGameLost();
        } else if (isGameWon && !isGameOver) {
            waitForSeconds -= Time.deltaTime;
            if (waitForSeconds <= 0) {
                isGameWon = true;
                Time.timeScale = 0f;
                winPannel.SetActive(true);
                SoundSystem.PlayGameWon();
            }
        }
        CheckWin();
    }

    public void Pause() {
        SoundSystem.PlayButtonClick();
        pausePannel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume() {
        SoundSystem.PlayButtonClick();
        pausePannel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void MainMenu() {
        SoundSystem.PlayButtonClick();
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }

    public void Restart() {
        SoundSystem.PlayButtonClick();
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel() {
        SoundSystem.PlayButtonClick();
        int currentLevel = LevelManager.GetCurrentLevelID();
        currentLevel++;
        LevelManager.SetCurrentLevel(currentLevel);
        if (currentLevel > LevelManager.GetLastLevelID()) {
            SceneManager.LoadScene("Main Menu");
            Debug.Log("No More Levels");
        } else if (currentLevel < 10) {
            string nextLevel = "Level 0" + currentLevel;
            Debug.Log(nextLevel);
            SceneManager.LoadScene(nextLevel);
        }
        Time.timeScale = 1f;
    }

    public void GameStarted() {
        SoundSystem.PlayButtonClick();
        isGameStarted = true;
    }

    public static void UpdateWave(int id = 0) {
        if (id == 0) {
            SoundSystem.PlayNextWave();
            currWave++;
            currentWaveText.text = currWave.ToString();
        }
    }

    public static bool IsGameWon() {
        return isGameWon;
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
 