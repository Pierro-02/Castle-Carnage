using System.Collections;
using System.ComponentModel;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private int maxHealth;
    [SerializeField] private float attackRate;
    [SerializeField] private int damage;
    [SerializeField] private Image healthBar;

    private Animator animator;
    private EnemySpawner spawner;
    private bool isAttacking;
    private GameObject troopInRange;
    private int health;

    private void Start () {
        animator = GetComponentInChildren<Animator>();

        health = maxHealth;

        spawner = GetComponentInParent<EnemySpawner>();

        spawner.waves[spawner.getCurrentWaveIndex()].enemiesLeft--;
    }

    private void Update () {
        Move();

        if (IsKilled()) {
            Destroy(this.gameObject);
        }
    }

    private void Move() {
        agent.SetDestination(spawner.GetDestination().position);
    }

    private IEnumerator Attacking(GameObject obj) {
        Troop troop = obj.gameObject.GetComponent<Troop>();

        while (isAttacking) {
            troop.TakeDamage(damage);

            if (troop.IsKilled()) {
                Destroy(obj.gameObject);
                Resume();
            } 
            if (IsKilled()) {
                isAttacking = false;
                Destroy(this.gameObject);
            }

            yield return new WaitForSeconds(attackRate);
        }
    }

    public void TakeDamage(int damage) {
        health -= damage;

        UpdateHealth(health, maxHealth);
    }

    public bool IsKilled() {
        if (health <= 0) {
            return true;
        }
        return false;
    }

    public void InRange(GameObject troop) {
        isAttacking = true;
        troopInRange = troop;
        animator.SetBool("IsAttacking", true);

        agent.SetDestination(troop.transform.position);
        StartCoroutine(Attacking(troopInRange));
    }

    public void TroopDetected() {
        agent.isStopped = true;
    }

    private void Resume() {
        animator.SetBool("IsAttacking", false);
        isAttacking = false;
        Move();
        agent.isStopped = false;
    }

    private void UpdateHealth(float currentHealth, float maxHealth) {
        healthBar.fillAmount = currentHealth / maxHealth;
    }
}
