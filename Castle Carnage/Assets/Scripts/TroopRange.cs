using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopRange : MonoBehaviour {

    private Troop troop;

    private void Start () {
        troop = GetComponentInParent<Troop>();
    }

    private void OnTriggerStay(Collider other) {
        if (other.gameObject.tag == "Enemy") {
            Debug.Log("Enemy In Range");
            troop.inRange = true;
        }
    }
}
