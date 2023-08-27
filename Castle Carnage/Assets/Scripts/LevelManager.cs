using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {
    [SerializeField] int maxLevels;

	public Button[] buttons;

	private static int currentLevel;
    private static int lastLevel;

    private void Start() {
        currentLevel = 0;
        lastLevel = maxLevels;
    }

    public static void SetCurrentLevel(int level) {
        currentLevel = level;
    } 

    public void OpenLevel(int levelId) {
        this.GetComponent<AudioSource>().Play();

        currentLevel = levelId;

        string LevelName;
        if (levelId <= 9) {
            LevelName = "Level 0" + levelId;
        } else {
            LevelName = "Level " + levelId;
        }
		Debug.Log(LevelName);

		SceneManager.LoadScene(LevelName);
	}

	public static int GetCurrentLevelID() {
		return currentLevel; 
	}

    public static int GetLastLevelID() {
        return lastLevel;
    }
}
