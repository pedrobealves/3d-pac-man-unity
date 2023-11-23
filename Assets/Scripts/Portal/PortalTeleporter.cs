using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleporter : MonoBehaviour
{

    [SerializeField]
    private GameObject portalDestiny;
    //private GameObject player;


    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerStay(Collider other)
    {

        Vector3 otherFromPortal = transform.InverseTransformPoint(other.transform.position);

        if (otherFromPortal.z < 0.9)
        {
            other.transform.position = portalDestiny.transform.position + new Vector3(-otherFromPortal.x, +otherFromPortal.y, -otherFromPortal.z);
        }
    }
}
