using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnItems : MonoBehaviour
{

    [SerializeField] private GameObject capsule;
    [SerializeField] private int maxCapsules;

    private GameObject[] allPills;
    private CharacterStatus status;

    public int maxPills;


    // Start is called before the first frame update
    void Start()
    {
        status = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStatus>();
        GameEvents.instance.OnGetPill += CheckMaxPills;
        allPills = GameObject.FindGameObjectsWithTag("Pill");
        SpawnCapsules();
        maxPills = allPills.Length - maxCapsules;
    }

    void SpawnCapsules()
    {
        int lastRandomNumber = -1;

        for (int i = 0; i < maxCapsules; i++)
        {
            int randomNumber = lastRandomNumber;
            while (randomNumber == lastRandomNumber)
                randomNumber = Random.Range(0, allPills.Length);
            lastRandomNumber = randomNumber;
            Instantiate(capsule, allPills[randomNumber].transform.position, Quaternion.identity);
            allPills[randomNumber].SetActive(false);
        }
    }

    void DestroyAllCapsules()
    {
        GameObject[] capsules = GameObject.FindGameObjectsWithTag("Capsule");
        foreach (GameObject capsule in capsules)
        {
            Destroy(capsule);
        }
    }

    void CheckMaxPills()
    {
        if (status.pills >= maxPills)
        {
            Debug.Log("All pills collected!");
            foreach (GameObject pill in allPills)
            {
                Debug.Log("Pill activated!");
                pill.SetActive(true);
            }
            DestroyAllCapsules();
            SpawnCapsules();
            status.pills = 0;
        }
    }

}
