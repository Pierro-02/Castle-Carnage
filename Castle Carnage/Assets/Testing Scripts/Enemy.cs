using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.Utilities;

public class Enemy : MonoBehaviour {

    [SerializeField] private NavMeshAgent agent;
    
    private EnemySpawner spawner;

    private void Start () {
        spawner = GetComponentInParent<EnemySpawner>();

        spawner.waves[spawner.getCurrentWaveIndex()].enemiesLeft--;
    }

    private void Update () {
        Move();
    }

    private void Move() {
        agent.SetDestination(spawner.GetDestination().position);
    }
}
