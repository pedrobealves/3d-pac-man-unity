using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStatus : MonoBehaviour
{
    [SerializeField]
    private float maxHealth = 10f;
    private float currentHealth;
    private Animator anim;
    public bool isDead = false;
    private NavMeshAgent navMeshAgent;
    [SerializeField]
    private Transform respawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponentInChildren<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void TakeDamage(float damage)
    {
        if (isDead)
        {
            return;
        }

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            isDead = true;
            navMeshAgent.isStopped = true;
            anim.SetTrigger("Die");
            Invoke("Respawn", 5f);
        }
    }

    private void Respawn()
    {
        transform.position = respawnPoint.position;
        anim.ResetTrigger("Die");
        navMeshAgent.isStopped = false;
        currentHealth = maxHealth;
        isDead = false;
    }

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Debug.Log("Enemy hit!");
            TakeDamage(1f);
        }
    }
}
