using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : MonoBehaviour {

    [SerializeField] private AudioSource[] backGroundMusic;
    [SerializeField] private AudioSource[] m_deathSound;
    [SerializeField] private AudioSource m_lifeLost;
    [SerializeField] private AudioSource m_gameWon;
    [SerializeField] private AudioSource m_gameLost;
    [SerializeField] private AudioSource m_nextWave;

    private static List<AudioSource> deathSound;
    private static AudioSource lifeLost;
    private static AudioSource gameWon;
    private static AudioSource gameLost;
    private static AudioSource nextWave;
    private int index;

    private void Start() {
        deathSound = new List<AudioSource>();
        lifeLost = m_lifeLost;
        gameWon = m_gameWon;
        gameLost = m_gameLost;
        nextWave = m_nextWave;

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
}
