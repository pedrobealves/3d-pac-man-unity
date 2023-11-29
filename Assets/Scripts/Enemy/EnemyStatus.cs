using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStatus : MonoBehaviour
{
    [SerializeField] private float maxHealth = 10f;
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private AudioSource dieAudio;
    [SerializeField] public AudioSource idleAudio;

    private float currentHealth;
    private Animator anim;
    private NavMeshAgent navMeshAgent;
    private EnemyMovement enemyMovement;

    public bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponentInChildren<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyMovement = GetComponent<EnemyMovement>();
        idleAudio.Play();
    }

    public void TakeDamage(float damage)
    {
        idleAudio.Stop();

        if (isDead)
        {
            return;
        }

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            dieAudio.Play();
            isDead = true;
            navMeshAgent.isStopped = true;
            anim.SetTrigger("Die");
            Invoke("Respawn", 5f);
        }
    }

    private void Respawn()
    {
        idleAudio.Play();
        transform.position = respawnPoint.position;
        anim.ResetTrigger("Die");
        anim.Play("CharacterArmature|Idle");
        navMeshAgent.isStopped = false;
        currentHealth = maxHealth;
        isDead = false;
    }

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            enemyMovement.currentState = EnemyMovement.EnemyState.Chase;
            TakeDamage(1f);
            Destroy(other.gameObject);
            Debug.Log("Enemy hit!");
        }
    }
}
