using TMPro;
using UnityEngine;

public class Economy : MonoBehaviour {

    [SerializeField] private int initialAmount;
    [SerializeField] private TMP_Text text;

    private static int newAmount;
    private int currentAmount;

    private void Start() {
        newAmount = initialAmount;
        currentAmount = initialAmount;
        text.text = currentAmount.ToString();
    }

    private void Update() {
        if (newAmount != currentAmount) {
            if (newAmount < currentAmount)
                currentAmount -= 1;
            else 
                currentAmount += 1;
            text.text = currentAmount.ToString();
        }
    }

    public static void AddCoins(int val) {
        newAmount += val;
    }

    public static void SubtractCoins(int val) {
        newAmount -= val;
    }

    public static int GetCurrentCoins() {
        return newAmount;
    }
}
