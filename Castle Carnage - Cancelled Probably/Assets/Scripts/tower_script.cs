using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTower : MonoBehaviour {
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private float attackCooldown = 1f;
    //[SerializeField] private float arrowSpeed = 10f;
    [SerializeField] private float attackRange = 10f;

    private float timeSinceLastShot = 0f;
    private GameObject target;

    private void Update() {
        // Check if cooldown has passed and reset the timeSinceLastShot
        if (Time.time - timeSinceLastShot >= attackCooldown) {
            timeSinceLastShot = Time.time;

            // If there's a target within range, shoot an arrow
            if (target != null && Vector3.Distance(transform.position, target.transform.position) <= attackRange) {
                transform.LookAt(target.transform);
                ShootArrow();
            }
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Enemy") && target == null) {
            target = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (target != null && other.gameObject == target) {
            target = null;
        }
    }

    private void ShootArrow() {
        GameObject arrow = Instantiate(arrowPrefab, shootingPoint.position, shootingPoint.rotation);
        arrow.GetComponent<ArrowScript>().EnemyEntered(target);
    }
}
