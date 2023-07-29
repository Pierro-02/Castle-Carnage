using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour {


    [SerializeField] private float moveSpeed = 5f;

    private EnemySpawner spawner;
    private Vector3 moveDir = Vector3.forward;
    private float countdown = 5f;

    private void Start () {
        spawner = GetComponentInParent<EnemySpawner>();
    }

    private void Update() {
        Move();
        
        countdown -= Time.deltaTime;

        if(countdown <= 0) { 
            Destroy(gameObject);

            spawner.waves[spawner.currentWaveIndex].enemiesLeft--;
        }
    }

    private void Move() {
        transform.Translate(moveDir * moveSpeed * Time.deltaTime);
    }

}
