using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Economy : MonoBehaviour {

    [SerializeField] private TMP_Text text;
    [SerializeField] private int initialAmount;

    private int amount;
    //public bool add;

    private void Start() {
        amount = 0;
        AddCoins(initialAmount);
    }

    public void AddCoins(int val) {
        amount += val;
        text.text = amount.ToString();
    }

    public void SubtractCoins(int val) {
        amount -= val;
        text.text = amount.ToString();
    }

    public int GetCurrentCoins() {
        return amount;
    }
}
