using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour {
    [SerializeField] private List<Image> imageArr;
    
    private static int updateLives;
    private int currentLives;

    private void Start() {
        updateLives = imageArr.Count;
        currentLives = updateLives;
    }

    private void FixedUpdate() {
        if (currentLives != updateLives) {
            for (int i = 0; i < (currentLives - updateLives); i++) {
                imageArr[imageArr.Count - 1].gameObject.SetActive(false);
                imageArr.RemoveAt(imageArr.Count - 1);
            }
            currentLives = updateLives;
        }
    }

    public static void LifeLost(int amount = 1) {
        updateLives -= amount;
    }
}
