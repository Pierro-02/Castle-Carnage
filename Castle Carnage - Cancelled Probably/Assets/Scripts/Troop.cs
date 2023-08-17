
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Troop : MonoBehaviour {

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private int health;
    [SerializeField] private int damage;
    [SerializeField] private float attackRate;

    private Transform targetLocation;
    private GameObject target;
    [SerializeField] private Transform spawn;

    // Start is called before the first frame update
    void Start() {
        SetDestination(spawn);
    }

    // Update is called once per frame
    void FixedUpdate() {

    }

    public bool IsKilled() {
        return (health <= 0);
    } 

    public void TakeDamage(int damage) {
        health -= damage;
    }

    public void SetDestination(Transform des) {
        spawn = des;
        targetLocation = des;
    }

    public NavMeshAgent GetAgent() {
        return agent;
    }
}
