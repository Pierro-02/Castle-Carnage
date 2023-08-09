using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TowerPlacer : MonoBehaviour {
    
    RaycastHit hit;
    Vector3 movePoint;
    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject economyManager;
    [SerializeField] private int prefabPrice;

    private bool canPlace;
    private Economy eco;

    // Start is called before the first frame update
    void Start() {
        canPlace = false;
        eco = economyManager.GetComponent<Economy>();
    }

    // Update is called once per frame
    void Update() {
        //Debug.Log("Touch Count: " + Input.touchCount + " Can Place: " + canPlace);
        if (canPlace && Input.touchCount > 0) {

            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

            if (Physics.Raycast(ray, out hit)) {
                if (hit.collider.gameObject.layer == 11 && eco.GetCurrentCoins() >= prefabPrice) {
                    canPlace = false;

                    eco.SubtractCoins(prefabPrice);

                    hit.collider.gameObject.layer = 12;

                    GameObject tower = Instantiate(prefab);

                    tower.transform.SetParent(hit.collider.gameObject.transform);

                    tower.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

                    tower.transform.localPosition = new Vector3(0, 1, 0);
                }
            }

            
        }
    }

    public void CanPlace() {
        canPlace = true;
    }
}
