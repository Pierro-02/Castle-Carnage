using UnityEngine;

public class NodeUI : MonoBehaviour {

    private void Start() {
        Deactivate();
    }

    public void SetPosition(Vector3 pos) {
        pos.y += 0.5f;
        transform.position = pos;
    }
    public void Activate() { 
        gameObject.SetActive(true);
    }

    public void Deactivate() {
        gameObject.SetActive(false);
    }
}
