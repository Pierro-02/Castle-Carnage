using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour {
    [SerializeField] private Image _healthBar;
    [SerializeField] private float _maxHealth;
    [SerializeField] private GameObject[] stars;

    private static float currentHealth;

    private static float maxHealth;
    private static Image healthBar;
    private Canvas canvas;
    private int starCount;

    private void Start() {
        starCount = 3;
        maxHealth = _maxHealth;
        currentHealth = maxHealth;
        healthBar = _healthBar;
        canvas = _healthBar.GetComponentInParent<Canvas>();
        canvas.transform.rotation = Quaternion.LookRotation(canvas.transform.position - Camera.main.transform.position);
    }

    private void Update() {
        if (starCount == 1 || healthBar.fillAmount == 1) {
            return;
        } else if (starCount == 3 && healthBar.fillAmount < 0.8) {
            starCount--;
            stars[2].SetActive(false);
        } else if (starCount == 2 && healthBar.fillAmount < 0.5) {
            starCount--;
            stars[1].SetActive(false);
        }
    }

    public static void LifeLost(int life = 1) {
        currentHealth -= life;

        Debug.Log(currentHealth);
        Debug.Log("Game Over: " + IsGameOver());

        healthBar.fillAmount = currentHealth / maxHealth;
    }

    public static bool IsGameOver() {
        if (currentHealth <= 0)
            return true;
        return false;
    }
}
