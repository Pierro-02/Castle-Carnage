using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private int maxHealth;
    [SerializeField] private float attackRate;
    [SerializeField] private int damage;
    [SerializeField] private Image healthBar;
    [SerializeField] private int coinsOnDeath;

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
            Economy.AddCoins(coinsOnDeath);
            Destroy(this.gameObject);
        }
    }

    private void Move() {
        agent.SetDestination(spawner.GetDestination().position);
    }

    public void TakeDamage(int damage) {
        health -= damage;
        UpdateHealth(health, maxHealth);
    }

    public bool IsKilled() {
        return (health <= 0);
    }

    private void UpdateHealth(float currentHealth, float maxHealth) {
        healthBar.fillAmount = currentHealth / maxHealth;
    }
}
