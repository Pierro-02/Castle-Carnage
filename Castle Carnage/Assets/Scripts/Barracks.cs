using UnityEngine;

public class Barracks : MonoBehaviour {

    [SerializeField] private GameObject troop;
    [SerializeField] private Transform[] spawnLocations;

    private bool start;
    private float waitTime;

    private void Start() {
        start = false;
        waitTime = 1;
    }

    // Update is called once per frame
    void Update() {
        if (start) {
            waitTime -= Time.deltaTime;
            if (waitTime <= 0) {
                start = false;
                Spawn();
            }
        }
    }

    private void Spawn() {
        for (int i = 0; i < spawnLocations.Length; i++) {
            GameObject _troop = Instantiate(troop);

            _troop.GetComponent<Troop>().GetAgent().Warp(spawnLocations[i].position);

            _troop.transform.SetParent(spawnLocations[i].transform, true);

            _troop.GetComponent<Troop>().SetDestination(spawnLocations[i]);
        }
    }

    public void _Start() {
        start = true;
    }

    private void Respawn() {

    }
}
