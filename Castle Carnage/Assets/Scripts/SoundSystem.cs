using UnityEngine;

public class SoundSystem : MonoBehaviour {

    [SerializeField] private AudioSource[] backGroundMusic;
    [SerializeField] private AudioSource m_deathSound;

    private static AudioSource deathSound;
    private int index;

    private void Start() {
        deathSound = m_deathSound;
        
        index = Random.Range(0, backGroundMusic.Length);

        Debug.Log(index);

        backGroundMusic[index].Play(); 
    }

    private void FixedUpdate() {
        if (!backGroundMusic[index].isPlaying) {
            index = Random.Range(0, backGroundMusic.Length);
            backGroundMusic[index].Play();
        }
    }

    public static void PlayDeath() {
        deathSound.Play();
    }
}
