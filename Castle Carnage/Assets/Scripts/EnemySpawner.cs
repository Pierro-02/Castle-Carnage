using System.Collections;
using TMPro;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    private const string ENEMY_TAG = "Enemy";

    [SerializeField] private float countdown;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private GameObject destination;
    [SerializeField] private GameObject wavePreviewPannel;
    [SerializeField] private TMP_Text goblin1Count, goblin2Count, demonCount;
    [SerializeField] private int iD;


    public Wave[] waves;
    private int currentWaveIndex = 0;
    private bool readyToCountdown;

    private static bool gameReady = false;
    private bool wavesFinished;
    private int[] enemiesToSpawn;

    //[SerializeField] private NavMeshAgent navMeshAgent;
    //private NavMeshPath navMeshPath = new NavMeshPath();


    private void Start () {
        InitGoblinCounts(0);

        wavesFinished = false;

        gameReady = false;

        readyToCountdown = true;

        enemiesToSpawn = new int[waves.Length];

        //Debug.Log("Number of Waves: " + waves.Length);

        for (int i = 0; i < waves.Length; i++) {
            waves[i].enemiesLeft = waves[i].enemies.Length;

            enemiesToSpawn[i] = waves[i].enemies.Length;

            //Debug.Log(enemiesToSpawn[i]);
        }
    }

    private void Update() {
        //Debug.Log("Path Exists: " + PathChecker());

        if (!gameReady) {
            return;
        }

        if (currentWaveIndex >= waves.Length) {
            readyToCountdown = false;
            return;
        }

        if (waves[currentWaveIndex].enemiesLeft == 0) {
            readyToCountdown = true;

            currentWaveIndex++;
            if (currentWaveIndex >= waves.Length)
                InitGoblinCounts(currentWaveIndex);
        }

        if (readyToCountdown == true) {
            countdown -= Time.deltaTime;
        }

        if (countdown <= 0) {
            GameManager.UpdateWave(iD);
            readyToCountdown = false;

            countdown = waves[currentWaveIndex].timeToNextWave;
            StartCoroutine(SpawnWave());
        }
    }

    private bool PathChecker() {
        //if (navMeshAgent.CalculatePath(destination.gameObject.transform.position, navMeshPath) && navMeshPath.status == NavMeshPathStatus.PathComplete) {
        //    return true;
        //}
        return false;
    }

    private IEnumerator SpawnWave() {
        //Debug.Log(currentWaveIndex);
        if (currentWaveIndex < waves.Length) {
            int length = enemiesToSpawn[currentWaveIndex];
            for (int i = 0; i < length; i++) {
                //Debug.Log("i: " + i);
                Enemy enemy = Instantiate(waves[currentWaveIndex].enemies[i], spawnPoint.transform);

                enemy.transform.SetParent(spawnPoint.transform);

                yield return new WaitForSeconds(waves[currentWaveIndex].timeToNextEnemy);
            }
        }
    }

    public int GetCurrentWaveIndex() {
        return currentWaveIndex;
    }

    public int GetMaxWaveIndex() {
        return waves.Length;
    }

    public Transform GetDestination() {
        return destination.transform;
    }

    public static void StartGame() {
        if (CubePlacer.GetIsPathComplete()) {
            gameReady = true;
        }
    }

    public bool WavesFinished() {
        return wavesFinished;
    }

    private void InitGoblinCounts(int waveID) {
        int gob1 = 0, gob2 = 0, dem = 0;
        foreach (Enemy enemy in waves[waveID].enemies) {
            int id = enemy.GetID();
            switch (id) {
                case 0:
                    gob1++;
                    break;
                case 1:
                    gob2++;
                    break;
                case 2:
                    dem++;
                    break;
                default:
                    break;
            }
        }

        goblin1Count.text = gob1.ToString();
        goblin2Count.text = gob2.ToString();
        demonCount.text = dem.ToString();
    }
}


[System.Serializable]
public class Wave {
    public Enemy[] enemies;
    public float timeToNextEnemy = 2;
    public float timeToNextWave = 2;

    [HideInInspector] public int enemiesLeft;
}
