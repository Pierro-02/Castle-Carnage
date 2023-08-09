using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : MonoBehaviour {

    [SerializeField] private GameObject troop;
    [SerializeField] private Transform[] spawnLocations;


    private void Start () {
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
           
    }

    private void Spawn() {
        for (int i = 0; i < spawnLocations.Length; i++) {
            GameObject _troop = Instantiate(troop);

            _troop.transform.position = spawnLocations[i].position;

            _troop.transform.SetParent(spawnLocations[i].transform, true);
        }
    }

    private void Respawn() {

    }
}
