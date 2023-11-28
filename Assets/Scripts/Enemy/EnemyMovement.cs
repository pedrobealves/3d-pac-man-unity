using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private GameObject player;
    private Transform currentWaypoint;

    [SerializeField]
    private float maxDistanceFollow = 10f;
    [SerializeField]
    private Waypoints waypoints;
    [SerializeField]
    private float idleTime = 2f;
    [SerializeField]
    private float walkSpeed = 3.5f;
    [SerializeField]
    private float chaseSpeed = 4f;
    [SerializeField]
    private Animator anim;
    private float idleTimer = 0f;
    private EnemyStatus status;

    public enum EnemyState { Idle, Walk, Chase }
    public EnemyState currentState = EnemyState.Idle;


    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint);
        anim = GetComponentInChildren<Animator>();
        status = GetComponent<EnemyStatus>();
    }

    private void next()
    {
        navMeshAgent.SetDestination(currentWaypoint.transform.position);
        currentState = EnemyState.Walk;
        navMeshAgent.speed = walkSpeed; // Set the walking speed.
        currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint);
    }

    // Update is called once per frame
    void Update()
    {
        if (status.isDead)
        {
            return;
        }
        switch (currentState)
        {
            case EnemyState.Idle:
                idleTimer += Time.deltaTime;
                anim.SetBool("IsWalking", false);
                anim.SetBool("IsChasing", false);

                if (idleTimer >= idleTime)
                {
                    next();
                }

                CheckForPlayerDetection();
                break;

            case EnemyState.Walk:
                idleTimer = 0f;
                anim.SetBool("IsWalking", true);
                anim.SetBool("IsChasing", false);
                if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
                {
                    currentState = EnemyState.Idle;
                }

                CheckForPlayerDetection();
                break;

            case EnemyState.Chase:
                idleTimer = 0f;
                navMeshAgent.speed = chaseSpeed; // Set the chase speed.
                navMeshAgent.SetDestination(player.transform.position);
                anim.SetBool("IsChasing", true);
                // Check if the player is out of sight and go back to the walk state.
                if (Vector3.Distance(transform.position, player.transform.position) > maxDistanceFollow)
                {
                    currentState = EnemyState.Walk;
                    navMeshAgent.speed = walkSpeed; // Restore walking speed.
                }
                break;
        }
    }

    private void CheckForPlayerDetection()
    {
        RaycastHit hit;
        Vector3 playerDirection = player.transform.position - transform.position;

        Debug.DrawRay(transform.position, playerDirection.normalized * maxDistanceFollow, Color.blue);

        if (Physics.Raycast(transform.position, playerDirection.normalized, out hit, maxDistanceFollow))
        {
            if (hit.collider.CompareTag("Player"))
            {
                currentState = EnemyState.Chase;
                Debug.Log("Player detected!");
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = currentState == EnemyState.Chase ? Color.red : Color.green;
        if (player != null)
            Gizmos.DrawLine(transform.position, player.transform.position);
    }

}
