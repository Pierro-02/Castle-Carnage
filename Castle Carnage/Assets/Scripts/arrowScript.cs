using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    [SerializeField] private float arrowSpeed = 10f; // The speed of the arrow
    [SerializeField] private float maxLifetime = 5f; // The maximum lifetime of the arrow (in seconds)
    [SerializeField] private int projectileDamage;

    private Rigidbody arrow;
    private float currentLifetime = 0f;
    private bool enemyLocated;
    private GameObject enemy = null;

    private void Start() {
        arrow = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        if (enemy != null) {
            gameObject.transform.LookAt(enemy.transform.position);
            transform.Translate(Vector3.forward * arrowSpeed * Time.deltaTime);
            currentLifetime += Time.deltaTime;
            arrow.velocity = arrow.transform.forward * arrowSpeed;
        } else {
            Destroy(gameObject);
        }
    }

    public void EnemyEntered(GameObject m_enemy) {
        enemy = m_enemy;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject == enemy) {
            enemy.GetComponent<Enemy>().TakeDamage(projectileDamage);
            Destroy(gameObject);
        }
    }
}
