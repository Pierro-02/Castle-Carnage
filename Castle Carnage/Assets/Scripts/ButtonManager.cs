using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour {

    [SerializeField] private GameObject[] buttonsToHide;

    public void Hide() {
        if (CubePlacer.GetIsPathComplete()) {
            foreach (var button in buttonsToHide) {
                button.GetComponent<Button>().interactable = false;
            }
        }
    }
}
