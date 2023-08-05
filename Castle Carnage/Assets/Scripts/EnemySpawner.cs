using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    private const string ENEMY_TAG = "Enemy";

    [SerializeField] private float countdown;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private GameObject destination;
    
    
    public Wave[] waves;
    private int currentWaveIndex = 0;
    private bool readyToCountdown;

    private int[] enemiesToSpawn;

    private void Start () {
        readyToCountdown = false;

        enemiesToSpawn = new int[waves.Length];

        Debug.Log("Number of Waves: " + waves.Length);

        for (int i = 0; i < waves.Length; i++) {
            waves[i].enemiesLeft = waves[i].enemies.Length;

            enemiesToSpawn[i] = waves[i].enemies.Length;

            Debug.Log(enemiesToSpawn[i]);
        }

        
    }

    private void Update() {
        if (currentWaveIndex >= waves.Length) {
            readyToCountdown = false;
            return;
        }

        if (waves[currentWaveIndex].enemiesLeft == 0) {
            readyToCountdown = true;

            currentWaveIndex++;
        }

        if (readyToCountdown == true) {
            countdown -= Time.deltaTime;
        }

        if (countdown <= 0) {
            readyToCountdown = false;

            countdown = waves[currentWaveIndex].timeToNextWave;

            StartCoroutine(SpawnWave());
        }
    }

    private IEnumerator SpawnWave() {
        Debug.Log(currentWaveIndex);
        if (currentWaveIndex < waves.Length) {
            for (int i = 0; i < enemiesToSpawn[currentWaveIndex]; i++) {
                Debug.Log("i: " + i);
                Enemy enemy = Instantiate(waves[currentWaveIndex].enemies[i], spawnPoint.transform);

                enemy.transform.SetParent(spawnPoint.transform);

                yield return new WaitForSeconds(waves[currentWaveIndex].timeToNextEnemy);
            }
        }
    }

    public int getCurrentWaveIndex() {
        return currentWaveIndex;
    }

    public Transform GetDestination() {
        return destination.transform;
    }

    public void StartGame() {
        Debug.Log("Testing");
        readyToCountdown = true;
    }
}


[System.Serializable]
public class Wave {
    public Enemy[] enemies;
    public float timeToNextEnemy = 2;
    public float timeToNextWave = 2;

    [HideInInspector] public int enemiesLeft;
}
