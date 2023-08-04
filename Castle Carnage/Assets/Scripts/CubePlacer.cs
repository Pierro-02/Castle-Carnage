using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class CubePlacer : MonoBehaviour {

    [SerializeField] private GameObject objectToCreate;
    [SerializeField] private GameObject parentGrid;
    [SerializeField] private NavmeshBaker meshBaker;

    private Grid grid;

    private bool touching;
    private bool placing;
    private bool deleting;

    private void Awake() {
        placing = false;
        touching = false;
        grid = FindObjectOfType<Grid>();
    }

    private void Update() {
        if (touching == true || Input.GetMouseButton(0)) {
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

        if (Physics.Raycast(ray, out hitInfo)) {
            if (hitInfo.collider.gameObject.layer == 9) {
                PlaceCubeNear(hitInfo.point);
            }

        }
    }

    private void Remove() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo)) {
            int hitLayer = hitInfo.collider.gameObject.layer;
            if (hitLayer != 9) {
                Destroy(hitInfo.collider.gameObject);
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
}
