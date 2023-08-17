using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class TowerPlacer : MonoBehaviour {
    
    RaycastHit hit;
    Vector3 movePoint;
    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject economyManager;
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private int prefabPrice;
    [SerializeField] private LayerMask layersToInclude;

    private bool canPlace;
    private Economy eco;

    // Start is called before the first frame update
    void Start() {
        canPlace = false;
        eco = economyManager.GetComponent<Economy>();
        UpdatePrice(prefabPrice);
    }

    // Update is called once per frame
    void Update() {
        //Debug.Log("Touch Count: " + Input.touchCount + " Can Place: " + canPlace);
        if (canPlace && Input.touchCount > 0) {

            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layersToInclude)) {
                if (hit.collider.gameObject.layer == 11 && eco.GetCurrentCoins() >= prefabPrice) {
                    canPlace = false;

                    eco.SubtractCoins(prefabPrice);

                    hit.collider.gameObject.layer = 12;

                    prefabPrice += (int)((float)prefabPrice / 2f);
                    UpdatePrice(prefabPrice);

                    GameObject tower = Instantiate(prefab);

                    tower.transform.SetParent(hit.collider.gameObject.transform);

                    tower.transform.localScale = new Vector3(0.45f, 0.3f, 0.45f);

                    tower.transform.localPosition = new Vector3(0, 0.55f, 0);
                }
            }
        }
    }

    private void UpdatePrice(int price) {
        priceText.text = price.ToString();
    }

    public void CanPlace() {
        canPlace = true;
    }
}
