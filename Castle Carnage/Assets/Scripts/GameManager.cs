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

    private void Start() {
        gameOverPannel.SetActive(false);
        pausePannel.SetActive(false);
        currWave = 0;
        currentWaveText = _currentWave;
        maxWave.text = waveSpawner[0].GetMaxWaveIndex().ToString();
        isGameStarted = false;
        isGameOver = false;
        isGameWon = false;
    }

    private void FixedUpdate() {
        if (isGameWon) {
            return;
        }

        if (HealthManager.IsGameOver() && !isGameOver) {
            isGameOver = true;
            Time.timeScale = 0f;
            gameOverPannel.SetActive(true);
        }
        //if (!isGameWon) {
        //    isGameWon = true;
        //    foreach (var wave in waveSpawner) {
                
        //    }
        //}
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
            SceneManager.LoadScene(nextLevel);
        }
    }

    public void GameStarted() {
        isGameStarted = true;
    }

    private void TowerUpgrade() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitPoint;

        if (Physics.Raycast(ray, out hitPoint, Mathf.Infinity, upgradableLayers)) {
            //Show Upgrade
        }
    }

    public static void UpdateWave(int id = 0) {
        if (id == 0) {
            currWave++;
            currentWaveText.text = currWave.ToString();
        }
    }
}
 