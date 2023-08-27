using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerSell : MonoBehaviour {

    [SerializeField] LayerMask layersToInclude;
    [SerializeField] GameObject canvas;

    private Ray ray;
    private RaycastHit hit;

    private void Start() {
        Hide();
    }

    private void FixedUpdate() {
        if (Input.touchCount > 0) {
            ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layersToInclude)) {
                if (hit.collider.gameObject.layer == 12) {
                    hit.collider.gameObject.GetComponent<TowerSell>().Show();
                } else if (hit.collider.gameObject.layer != 14) {
                    Debug.Log(hit.collider.gameObject.layer);
                    Hide();   
                }
            }
        }
    }


    public void Show() {
        canvas.SetActive(true);
    }

    public void Hide() {
        canvas.SetActive(false);
    }

    public void DeleteTower() {
        Debug.Log("Tower Deleted");
        Economy.AddCoins(75);
        this.gameObject.GetComponentInChildren<ArcherTower>().Delete();
        this.gameObject.layer = 11;
        Hide();
    }
}
