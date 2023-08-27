using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : MonoBehaviour {

    [SerializeField] private AudioSource[] backGroundMusic;
    [SerializeField] private AudioSource[] m_deathSound;
    [SerializeField] private AudioSource m_lifeLost;
    [SerializeField] private AudioSource m_gameWon;
    [SerializeField] private AudioSource m_gameLost;
    [SerializeField] private AudioSource m_nextWave;
    [SerializeField] private AudioSource m_buttonClick;
    [SerializeField] private AudioSource m_pathPlace;
    [SerializeField] private AudioSource m_pathSell;
    [SerializeField] private AudioSource m_towerPlace;

    private static List<AudioSource> deathSound;
    private static AudioSource lifeLost;
    private static AudioSource gameWon;
    private static AudioSource gameLost;
    private static AudioSource nextWave;
    private static AudioSource buttonClick;
    private static AudioSource pathPlace;
    private static AudioSource pathSell;
    private static AudioSource towerPlace;
    private int index;

    private void Start() {
        deathSound = new List<AudioSource>();
        lifeLost = m_lifeLost;
        gameWon = m_gameWon;
        gameLost = m_gameLost;
        nextWave = m_nextWave;
        buttonClick = m_buttonClick;
        pathPlace = m_pathPlace;
        pathSell = m_pathSell;
        buttonClick = m_buttonClick;
        towerPlace = m_towerPlace;

        foreach (var sound in m_deathSound)
            deathSound.Add(sound);
        
        index = Random.Range(0, backGroundMusic.Length);

        Debug.Log(index);

        backGroundMusic[index].Play(); 
    }

    private void FixedUpdate() {
        if (HealthManager.IsGameOver()) {
            backGroundMusic[index].Stop();
            return;
        }
        if (!backGroundMusic[index].isPlaying) {
            index = Random.Range(0, backGroundMusic.Length);
            backGroundMusic[index].Play();
        }
    }

    public static void PlayDeath() {
        int index = Random.Range(0, deathSound.Count);
        deathSound[index].Play();
    }

    public static void PlayLifeLost() {
        lifeLost.Play();
    }

    public static void PlayGameWon() {
        gameWon.Play();
    }

    public static void PlayGameLost() {
        gameLost.Play();
    }

    public static void PlayNextWave() {
        nextWave.Play();
    }

    public static void PlayPathPlace() {
        pathPlace.Play();
    }

    public static void PlayPathSell() {
        pathSell.Play();
    }

    public static void PlayTowerPlace() {
        towerPlace.Play();
    }

    public static void PlayButtonClick() {
        buttonClick.Play();
    }
}
