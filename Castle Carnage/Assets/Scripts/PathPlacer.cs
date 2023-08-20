using TMPro;
using UnityEngine;


public class CubePlacer : MonoBehaviour {

    [SerializeField] private GameObject objectToCreate;
    [SerializeField] private GameObject parentGrid;
    [SerializeField] private NavmeshBaker meshBaker;
    [SerializeField] private int pathPrice;
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private int pathCounter;
    [SerializeField] private TMP_Text counterText;
    [SerializeField] private LayerMask layersToInclude, pathLayers, endLayer;

    private Grid grid;
    private Economy eco;
    private bool touching;
    private bool placing;
    private bool deleting;
    private int initialCounterValue;
    private Vector3[] directions;

    private static bool isPathComplete = false;

    private void Awake() {
        isPathComplete = false;

        directions = new Vector3[4];

        directions[0] = Vector3.forward;
        directions[1] = Vector3.back;
        directions[2] = Vector3.left;
        directions[3] = Vector3.right;

        initialCounterValue = pathCounter;
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

                if (Valid(hitInfo.point)) {
                    pathCounter--;
                    UpdatePathCounter(pathCounter);
                    Economy.SubtractCoins(pathPrice);
                    PlaceCubeNear(hitInfo.point);
                }

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

    private bool Valid(Vector3 clickPoint) {
        bool testing = false;
        float dist = 0.27f;

        var point = grid.GetNearestPointOnGrid(clickPoint);

        foreach (Vector3 dir in directions) {
            if (Physics.Raycast(point, dir, dist, pathLayers)) {
                testing = true;
            }
        }

        foreach (Vector3 dir in directions) {
            if (Physics.Raycast(point, dir, dist, endLayer)) {
                isPathComplete = true;
            }
        }

        return testing;
    }

    public static bool GetIsPathComplete() {
        return isPathComplete;
    }

    //private void TestRay(Vector3 pos) {
    //    if (pos != Vector3.zero) {
    //        Debug.DrawRay(pos, Vector3.forward * dis, Color.red);
    //        Debug.DrawRay(pos, Vector3.back * dis, Color.red);
    //        Debug.DrawRay(pos, Vector3.left * dis, Color.red);
    //        Debug.DrawRay(pos, Vector3.right * dis, Color.red);
    //    }
    //}
}
