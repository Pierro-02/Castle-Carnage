using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private int maxHealth;
    [SerializeField] private int damage;
    [SerializeField] private Image healthBar;
    [SerializeField] private int coinsOnDeath;
    [SerializeField] private int iD;

    private Animator animator;
    private EnemySpawner spawner;
    private int health;
    private bool escaped;

    private Camera _cam;
    private Canvas canvas;

    private void Start () {
        escaped = false;

        _cam = Camera.main;

        canvas = healthBar.GetComponentInParent<Canvas>();

        animator = GetComponentInChildren<Animator>();

        health = maxHealth;

        spawner = GetComponentInParent<EnemySpawner>();

        spawner.waves[spawner.GetCurrentWaveIndex()].enemiesLeft--;
    }

    private void Update () {
        Move();

        if (IsKilled()) {
            SoundSystem.PlayDeath();
            Economy.AddCoins(coinsOnDeath);
            Destroy(this.gameObject);
        } else if (escaped) {
            TakeDamage(9999);
            Destroy(this.gameObject);
        }

        canvas.transform.rotation = Quaternion.LookRotation(canvas.transform.position - _cam.transform.position);
    }

    private void Move() {
        agent.SetDestination(spawner.GetDestination().position);
    }

    public int GetDamage() {
        return damage;
    }

    public void TakeDamage(int damage) {
        health -= damage;
        UpdateHealth(health, maxHealth);
    }

    public bool IsKilled() {
        return (health <= 0);
    }

    public void Escaped() {
        escaped = true;
    }

    private void UpdateHealth(float currentHealth, float maxHealth) {
        healthBar.fillAmount = currentHealth / maxHealth;
    }

    public int GetID() {
        return iD;
    }
}
