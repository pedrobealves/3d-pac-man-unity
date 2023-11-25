using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStatus : MonoBehaviour
{
    [SerializeField]
    private float maxHealth = 10f;
    [SerializeField]
    private Transform respawnPoint;

    private float currentHealth;
    private Animator anim;
    private NavMeshAgent navMeshAgent;

    public bool isDead = false;


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
            Destroy(other.gameObject);
        }
    }
}
