using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class CubePlacer : MonoBehaviour {

    [SerializeField] private GameObject objectToCreate;
    [SerializeField] private GameObject parentGrid;
    [SerializeField] private NavmeshBaker meshBaker;
    [SerializeField] private int pathPrice;
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private int pathCounter;
    [SerializeField] private TMP_Text counterText;
    [SerializeField] private LayerMask layersToInclude;

    private Grid grid;
    private Economy eco;
    private bool touching;
    private bool placing;
    private bool deleting;

    private void Awake() {
        placing = false;
        touching = false;

        grid = FindObjectOfType<Grid>();
        UpdatePrice(pathPrice);
        UpdatePathCounter(pathCounter);
    }

    private void FixedUpdate() {
        bool temp = true;
        if (touching == true || Input.GetMouseButton(0) && temp) {
            temp = false;
            if (placing == true) {
                Place();
            }
            if (deleting == true) {
                Remove();
            }
            touching = false;
        }
    }

    public void PlaceSelected() {
        placing = true;
        deleting = false;
    }
    public void DeleteSelected() {
        placing = false;
        deleting = true;
    }

    public void OnDeselect() {
        placing = false;
        deleting = false;
    }

    public void Touch() {
        touching = true;
    }

    private void Place() {
        RaycastHit hitInfo;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layersToInclude)) {
            if (hitInfo.collider.gameObject.layer == 9 && Economy.GetCurrentCoins() >= pathPrice && pathCounter > 0) {

                pathCounter--;
                UpdatePathCounter(pathCounter);
                Economy.SubtractCoins(pathPrice);
                PlaceCubeNear(hitInfo.point);

            }
        }
    }

    private void Remove() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layersToInclude)) {
            int hitLayer = hitInfo.collider.gameObject.layer;
            if (hitLayer == 6) {

                Economy.AddCoins(pathPrice);
                Destroy(hitInfo.collider.gameObject);
                pathCounter++;
                UpdatePathCounter(pathCounter);

            }
        }
    }

    private void PlaceCubeNear(Vector3 clickPoint) {

        var finalPosition = grid.GetNearestPointOnGrid(clickPoint);
        GameObject obj = Instantiate(objectToCreate);
        obj.transform.position = finalPosition;
        obj.transform.SetParent(parentGrid.transform, true);

    }

    public void StartGame() {
        meshBaker.Bake();
    }

    private void UpdatePrice(int price) {
        priceText.text = price.ToString();
    }

    private void UpdatePathCounter(int counter) {
        counterText.text = counter.ToString();
    }
}
