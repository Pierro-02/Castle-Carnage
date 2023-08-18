using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
	public Button[] buttons;
	private void Awake() {

		int unlockedlevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
		for (int i = 0; i < buttons.Length; i++) {
			buttons[i].interactable = false;
		}
		for (int i = 0; i < unlockedlevel; i++) {
			buttons[i].interactable = true;
		}

	}
	public void OpenLevel(int levelId) {

		string LevelName = "Level 0" + levelId;

		Debug.Log(LevelName);

		SceneManager.LoadScene(LevelName);
	}
}
