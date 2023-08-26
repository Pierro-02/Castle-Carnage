using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour {

    [SerializeField] private GameObject[] buttonsToHide;
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject archerButton, crystalButton;
    [SerializeField] private TowerPlacer towerPlacer;

    private bool isPathComplete;

    private void Awake() {
        isPathComplete = false;
        startButton.GetComponent<Button>().interactable = false;
    }

    private void Update() { 
        if (CubePlacer.GetIsPathComplete() && !isPathComplete) {
            isPathComplete = true;
            startButton.GetComponent<Button>().interactable = true;
        } else if (!CubePlacer.GetIsPathComplete()) {
            isPathComplete = false;
            startButton.GetComponent<Button>().interactable = false;
        }

        ArcherButtonCheck();
        CrystalButtonCheck();
    }

    public void Hide() {
        if (CubePlacer.GetIsPathComplete()) {
            foreach (var button in buttonsToHide) {
                button.GetComponent<Button>().interactable = false;
            }
        }
    }

    private void ArcherButtonCheck() {
        if (towerPlacer.GetArcherPrice() > Economy.GetCurrentCoins()) {
            archerButton.GetComponent<Button>().interactable = false;
        } else {
            archerButton.GetComponent<Button>().interactable = true;
        }
    }

    private void CrystalButtonCheck() {
        if (towerPlacer.GetCrystalPrice() > Economy.GetCurrentCoins()) {
            crystalButton.GetComponent<Button>().interactable = false;
        } else {
            crystalButton.GetComponent<Button>().interactable = true;
        }
    }
}
