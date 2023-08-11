using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour {

    [SerializeField] private GameObject[] buttonsToHide;

    public void Hide() {
        foreach (var button in buttonsToHide) {
            button.gameObject.SetActive(false);
        }
    }
}
