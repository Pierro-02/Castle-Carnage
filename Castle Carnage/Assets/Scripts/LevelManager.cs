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

    //private void Awake() {

    //	int unlockedlevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
    //	for (int i = 0; i < buttons.Length; i++) {
    //		buttons[i].interactable = false;
    //	}
    //	for (int i = 0; i < unlockedlevel; i++) {
    //		buttons[i].interactable = true;
    //	}

    //}
    public static void SetCurrentLevel(int level) {
        currentLevel = level;
    } 

    public void OpenLevel(int levelId) {
		currentLevel = levelId;

		string LevelName = "Level 0" + levelId;

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
