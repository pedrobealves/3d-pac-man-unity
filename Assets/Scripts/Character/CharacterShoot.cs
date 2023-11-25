using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterShoot : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private GameObject bulletPoint;

    [SerializeField]
    private float bulletSpeed = 2000f;

    private Animator anim;
    private CharacterStatus status;

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        status = GetComponent<CharacterStatus>();
    }

    void shoot()
    {
        if (status.bullets <= 0)
            return;

        status.bullets--;

        anim.SetTrigger("shoot");

        Vector2 spawnPos = Camera.main.ViewportToWorldPoint(new Vector2(0.5f, 0.5f));

        GameObject bullet = Instantiate(bulletPrefab, bulletPoint.transform.position, bulletPoint.transform.rotation);

        bullet.GetComponent<Rigidbody>().AddForce(bulletPoint.transform.forward * bulletSpeed);

        Destroy(bullet, 2f);

        GameEvents.instance.UpdateBullets();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) ||
           Input.GetMouseButtonDown(0))
        {

            shoot();
        }
    }
}
