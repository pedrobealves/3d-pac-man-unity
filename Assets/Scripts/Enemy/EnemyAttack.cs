using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField]
    private float distanceAttack = 2f;
    private GameObject player;
    private Animator anim;
    private EnemyStatus status;
    private bool isAttacking = false;

    [SerializeField] private AudioSource attackAudio;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponentInChildren<Animator>();
        status = GetComponent<EnemyStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!status.isDead)
            CheckForPlayerDetection();
    }

    private void CheckForPlayerDetection()
    {
        RaycastHit hit;
        Vector3 playerDirection = player.transform.position - transform.position;

        Debug.DrawRay(transform.position, playerDirection.normalized * distanceAttack, Color.cyan);

        if (Physics.Raycast(transform.position, playerDirection.normalized, out hit, distanceAttack))
        {
            if (hit.collider.CompareTag("Player"))
            {
                if (!isAttacking)
                {
                    isAttacking = true;
                    attackAudio.Play();
                    hit.collider.GetComponent<CharacterStatus>().TakeDamage(1);
                    anim.SetTrigger("Attack");
                }
            }
            else
            {
                isAttacking = false;
            }
        }
    }
}
