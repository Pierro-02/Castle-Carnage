using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour {

    private const string OBJECT_LAYER = "Enemy";

    private void OnTriggerEnter(Collider collider) {
        Debug.Log(collider.name + " should be deleted");
    }

    private void OnTriggerStay(Collider collider) {
        if (collider.gameObject.tag == OBJECT_LAYER) {
            Destroy(collider.gameObject);
        }
    }
}
