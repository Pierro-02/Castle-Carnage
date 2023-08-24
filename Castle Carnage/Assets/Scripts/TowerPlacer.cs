using TMPro;
using UnityEngine;

public class TowerPlacer : MonoBehaviour {
    
    RaycastHit hit;
    Vector3 movePoint;
    [SerializeField] private GameObject archerTower, crystalTower;
    [SerializeField] private TMP_Text archerPriceText, crystalPriceText;
    [SerializeField] private int archerPrice, crystalPrice;
    [SerializeField] private LayerMask layersToInclude;
    [SerializeField] private NodeUI nodeUI;

    private bool canPlace;
    private bool isArcherSelected;
    //private bool isCrystalSelected;

    // Start is called before the first frame update
    void Start() {
        isArcherSelected = false;
        //isCrystalSelected = false;

        canPlace = false;
        UpdatePrice(archerPrice, archerPriceText);
        UpdatePrice(crystalPrice, crystalPriceText);
    }

    // Update is called once per frame
    private void FixedUpdate() {
        //Debug.Log("Touch Count: " + Input.touchCount + " Can Place: " + canPlace);
        if (Input.touchCount > 0) {

            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layersToInclude)) {
                if (hit.collider.gameObject.layer == 11 && CheckPrice() && canPlace) {
                    GameObject tower;

                    if (isArcherSelected) {
                        tower = Instantiate(archerTower);
                        Economy.SubtractCoins(archerPrice);
                        archerPrice += (int)((float)archerPrice / 2f);
                        UpdatePrice(archerPrice, archerPriceText);
                    } else {
                        tower = Instantiate(crystalTower);
                        Economy.SubtractCoins(crystalPrice);
                        crystalPrice += (int)((float)crystalPrice / 2f);
                        UpdatePrice(crystalPrice, crystalPriceText);
                    }


                    hit.collider.gameObject.layer = 12;

                    tower.transform.SetParent(hit.collider.gameObject.transform);

                    tower.transform.localScale = new Vector3(0.45f, 0.3f, 0.45f);

                    tower.transform.localPosition = new Vector3(0, 0.55f, 0);
                } else if (hit.collider.gameObject.layer == 12 && !canPlace) {
                    Debug.Log("Tower Selected");
                    nodeUI.Activate();
                    nodeUI.SetPosition(hit.collider.gameObject.transform.position);

                } else {
                    Debug.Log(hit.collider.gameObject.layer);
                    nodeUI.Deactivate();
                }
            }
            canPlace = false;
        }
    }

    private void UpdatePrice(int price, TMP_Text priceText) {
        priceText.text = price.ToString();
    }

    public void ArcherSelected() {
        isArcherSelected = true;
        //isCrystalSelected = false;
        canPlace = true;
    }

    public void CrystalSelected() {
        //isCrystalSelected = true;
        isArcherSelected = false;
        canPlace = true;
    }

    private bool CheckPrice() {
        if (isArcherSelected) {
            return (Economy.GetCurrentCoins() >= archerPrice);
        } else {
            return (Economy.GetCurrentCoins() >= crystalPrice);
        }
    }
}
