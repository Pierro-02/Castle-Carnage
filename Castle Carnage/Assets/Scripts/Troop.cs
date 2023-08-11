using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.AI;

public class Troop : MonoBehaviour {

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private int health;
    [SerializeField] private int damage;
    [SerializeField] private float attackRate;

    private Transform targetLocation;
    private bool attacking;
    private List<GameObject> enemies;
    private Transform target;

    public bool inRange;

    // Start is called before the first frame update
    void Start() {
        targetLocation = transform;
        attacking = false;
        inRange = false;
        enemies = new List<GameObject>();
    }

    // Update is called once per frame
    void Update() {
        if (!IsKilled()) {
            agent.SetDestination(targetLocation.position);
            if (inRange) {
                agent.isStopped = true;
                EnemyDetected();
            }
        }
    }

    private void OnTriggerStay(Collider obj) {
        if (obj.gameObject.tag == "Enemy" && !attacking) {
            if (!enemies.Contains(obj.gameObject))
                enemies.Add(obj.gameObject);
            obj.gameObject.GetComponent<Enemy>().TroopDetected();
            targetLocation = obj.gameObject.transform;
        }
    }

    private void OnTriggerExit(Collider obj) {
        if (obj.gameObject.tag == "Enemy") { 
            enemies.Remove(obj.gameObject);
        }
    }

    private void EnemyDetected() {
        for (int i = 0; i < enemies.Count; i++) {
            Enemy enemy = enemies[i].GetComponent<Enemy>();

            attacking = true;

            enemy.InRange(this.gameObject);
            StartCoroutine(Attack(enemies[i].gameObject));
            enemies.Remove(enemies[i--]);
        }
    }

    private IEnumerator Attack(GameObject obj) {
        // Attack Enemy
        Enemy enemy = obj.GetComponent<Enemy>();

        while (attacking) {
            enemy.TakeDamage(damage);

            if (enemy.IsKilled()) {
                EnemyKilled();
                yield return new WaitForSeconds(1);
            } 
            if (IsKilled()) {
                attacking = false;
            }

            yield return new WaitForSeconds(attackRate);
        }
    }

    public void EnemyKilled() {
        if (!IsKilled()) {
            agent.isStopped = false;
        }
        targetLocation = target;
        attacking = false;
        inRange = false;
    }

    public bool IsKilled() {
        return (health <= 0);
    } 

    public void TakeDamage(int damage) {
        health -= damage;
    }

    public void SetDestination(Transform des) {
        target = des;
        targetLocation = des;
    }

    public NavMeshAgent GetAgent() {
        return agent;
    }
}
