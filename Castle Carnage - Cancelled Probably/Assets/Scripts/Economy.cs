using TMPro;
using UnityEngine;

public class Economy : MonoBehaviour {

    [SerializeField] private int initialAmount;
    [SerializeField] private TMP_Text text;

    private static int amount;

    private void Start() {
        amount = 0;
        AddCoins(initialAmount);
    }

    private void FixedUpdate() {
        text.text = amount.ToString();
    }

    public static void AddCoins(int val) {
        amount += val;
        
    }

    public static void SubtractCoins(int val) {
        amount -= val;
    }

    public static int GetCurrentCoins() {
        return amount;
    }
}
