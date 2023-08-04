using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PanAndClamp : MonoBehaviour {

    public float speed;
    private Vector3 initialPos;
    private bool canPan;

    private void Start() {
        initialPos = transform.position;
        canPan = false;
    }

    public void Update() {
        //If our finger is on the screen and it has moved from its start position than do the code
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved && canPan) {

            // Remembers the finger movement by storing it in a vector 2
            Vector2 TouchDeltaPosition = Input.GetTouch(0).deltaPosition;

            // Does the x and y calculation and moves the screen1
            transform.Translate(-TouchDeltaPosition.x * speed, -TouchDeltaPosition.y * speed, 0);


            float thisX = transform.position.x;
            float thisY = transform.position.y;
            float thisZ = transform.position.z;

            // Boundries
            transform.position = new Vector3(
                Mathf.Clamp(thisX, initialPos.x - 20f, initialPos.x + 20f),
                Mathf.Clamp(thisY, initialPos.y - 5f, initialPos.y + 5f),
                Mathf.Clamp(thisZ, initialPos.z - 20f, initialPos.z + 20f));
        }
    }

    public void EnablePanning() {
        canPan = true;
    }

    public void DisablePanning() {
        canPan = false;
    }
}
