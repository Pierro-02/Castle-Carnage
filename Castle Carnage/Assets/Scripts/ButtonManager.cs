using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour {

    [SerializeField] private GameObject[] buttonsToHide;
    [SerializeField] private GameObject startButton;

    private bool isPathComplete;

    private void Awake() {
        isPathComplete = false;
        startButton.GetComponent<Button>().interactable = false;
    }

    private void Update() { 
        if (CubePlacer.GetIsPathComplete() && !isPathComplete) {
            isPathComplete = true;
            startButton.GetComponent<Button>().interactable = true;
        }
    }

    public void Hide() {
        if (CubePlacer.GetIsPathComplete()) {
            foreach (var button in buttonsToHide) {
                button.GetComponent<Button>().interactable = false;
            }
        }
    }
}
