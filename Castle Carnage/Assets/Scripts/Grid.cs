using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    [SerializeField] private float size = 1f;
    [SerializeField] private int gridX = 40;
    [SerializeField] private int gridZ = 40;

    private void Start() {
        if (size < 1f) size = 1f;
        if (gridX < 1) gridX = 1;
        if (gridZ < 1) gridZ = 1;
    }

    public Vector3 GetNearestPointOnGrid(Vector3 position) {
        position -= transform.position;

        int xCount = Mathf.RoundToInt(position.x / size);
        float yCount = Mathf.RoundToInt(position.y / size);
        int zCount = Mathf.RoundToInt(position.z / size);

        Vector3 result = new Vector3(
            (float)xCount * size,
            (float)yCount * size,
            (float)zCount * size);

        result += transform.position;

        return result;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Vector3 pos = transform.position;
        for(float x = pos.x; x < gridX + pos.x; x += size) {
            for (float z = pos.z; z < gridZ + pos.z; z += size) {
                var point = GetNearestPointOnGrid(new Vector3(x, 1f, z));
                Gizmos.DrawCube(point, new Vector3(0.1f, 0.1f, 0.1f));
            }
        }
    }
}
