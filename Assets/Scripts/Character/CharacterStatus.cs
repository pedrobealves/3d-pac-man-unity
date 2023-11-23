using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 1;
    [SerializeField]
    private int life = 3;
    private int currentHealth;
    [SerializeField]
    private Transform respawnPoint;

    private int pills = 0;

    public int maxPills;

    public int points = 0;

    GameObject[] allPills;

    private bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        allPills = GameObject.FindGameObjectsWithTag("Pill");
        maxPills = allPills.Length;
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
            Invoke("Respawn", .8f);
            if (life <= 0)
            {
                Debug.Log("Game Over!");
            }
        }
    }

    private void Respawn()
    {
        transform.position = respawnPoint.position;
        currentHealth = maxHealth;
        isDead = false;
    }

    private void getPill(GameObject other)
    {
        pills++;
        other.SetActive(false);
        Debug.Log("Pill collected!");

        if (pills >= maxPills)
        {
            Debug.Log("All pills collected!");
            foreach (GameObject pill in allPills)
            {
                Debug.Log("Pill activated!");
                pill.SetActive(true);
            }
            pills = 0;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Pill")
        {
            getPill(other.gameObject);
        }
    }
}
