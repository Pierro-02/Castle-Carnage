using Unity.AI.Navigation;
using UnityEngine;

public class NavmeshBaker : MonoBehaviour {

    [SerializeField] private NavMeshSurface mesh;

    public void Bake() {
        mesh.BuildNavMesh();
    }
}
