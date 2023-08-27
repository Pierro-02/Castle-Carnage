using TMPro;
using UnityEngine;

public class TowerPlacer : MonoBehaviour {
    
    RaycastHit hit;
    Vector3 movePoint;
    [SerializeField] private GameObject archerTower, crystalTower;
    [SerializeField] private TMP_Text archerPriceText, crystalPriceText;
    [SerializeField] private int archerPrice, crystalPrice;
    [SerializeField] private LayerMask layersToInclude;

    private bool canPlace;
    private bool isArcherSelected;

    void Start() {
        isArcherSelected = false;

        canPlace = false;
        UpdatePrice(archerPrice, archerPriceText);
        UpdatePrice(crystalPrice, crystalPriceText);
    }

    private void FixedUpdate() {
        if (Input.touchCount > 0) {

            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layersToInclude)) {
                if (hit.collider.gameObject.layer == 11 && CheckPrice() && canPlace) {
                    GameObject tower;

                    if (isArcherSelected) {
                        tower = Instantiate(archerTower);
                        Economy.SubtractCoins(archerPrice);
                        archerPrice += (int)((float)archerPrice * 0.4f);
                        UpdatePrice(archerPrice, archerPriceText);
                    } else {
                        tower = Instantiate(crystalTower);
                        Economy.SubtractCoins(crystalPrice);
                        crystalPrice += (int)((float)crystalPrice * 0.2f);
                        UpdatePrice(crystalPrice, crystalPriceText);
                    }
                    SoundSystem.PlayTowerPlace();
                    hit.collider.gameObject.layer = 12;
                    tower.transform.SetParent(hit.collider.gameObject.transform);
                    tower.transform.localScale = new Vector3(0.45f, 0.3f, 0.45f);
                    tower.transform.localPosition = new Vector3(0, 0.55f, 0);
                }
            }
            canPlace = false;
        }
    }

    private void UpdatePrice(int price, TMP_Text priceText) {
        priceText.text = price.ToString();
    }

    public void ArcherSelected() {
        SoundSystem.PlayButtonClick();
        isArcherSelected = true;
        canPlace = true;
    }

    public void CrystalSelected() {
        SoundSystem.PlayButtonClick();
        isArcherSelected = false;
        canPlace = true;
    }

    public int GetArcherPrice() {
        return archerPrice;
    }

    public int GetCrystalPrice() {
        return crystalPrice;
    }

    private bool CheckPrice() {
        if (isArcherSelected) {
            return (Economy.GetCurrentCoins() >= archerPrice);
        } else {
            return (Economy.GetCurrentCoins() >= crystalPrice);
        }
    }
}
