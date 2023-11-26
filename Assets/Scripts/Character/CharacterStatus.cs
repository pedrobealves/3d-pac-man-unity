using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
    [SerializeField] private int maxHealth = 1;
    [SerializeField] public int maxBullets = 10;
    [SerializeField] public int maxLife = 3;
    [SerializeField] public int life;
    [SerializeField] private Transform respawnPoint;

    private int currentHealth;
    public int pills = 0;

    public bool isDead = false;
    public int points = 0;
    public int bullets;

    // Start is called before the first frame update
    void Awake()
    {
        currentHealth = maxHealth;
        life = maxLife;
        bullets = maxBullets;
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
            return;
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            isDead = true;
            life--;
            Invoke("Respawn", .7f);
            GameEvents.instance.UpdateLife();
            if (life <= 0)
            {
                Debug.Log("Game Over!");
                GameEvents.instance.GameOver();
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

    private void TakeBullet(int number)
    {
        bullets += number;

        if (bullets >= maxBullets)
        {
            bullets = maxBullets;
        }

        GameEvents.instance.UpdateBullets();
    }

    private void Respawn()
    {
        transform.position = respawnPoint.position;
        currentHealth = maxHealth;
        isDead = false;
    }

    private void GetPill()
    {
        pills++;
        points++;

        Debug.Log("Pill collected!");

        GameEvents.instance.CheckMaxPills();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Pill")
        {
            other.gameObject.SetActive(false);
            GetPill();
        }

        if (other.gameObject.tag == "Capsule")
        {
            TakeBullet(maxBullets);
            Destroy(other.gameObject);
        }
    }
}
