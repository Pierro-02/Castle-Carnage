using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour {
    [SerializeField] private Image _healthBar;
    [SerializeField] private float _maxHealth;

    private static float currentHealth;

    private static float maxHealth;
    private static Image healthBar;
    private Canvas canvas;

    private void Start() {
        maxHealth = _maxHealth;
        currentHealth = maxHealth;
        healthBar = _healthBar;
        canvas = _healthBar.GetComponentInParent<Canvas>();
        canvas.transform.rotation = Quaternion.LookRotation(canvas.transform.position - Camera.main.transform.position);
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
