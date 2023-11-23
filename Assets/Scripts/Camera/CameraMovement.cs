using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Quaternion rotIni;
    public float rotVel;
    private float contRotX = 0;

    // Start is called before the first frame update
    void Start()
    {
        rotIni = transform.localRotation;
        rotVel = 70;
    }

    // Update is called once per frame
    void Update()
    {
        contRotX += Input.GetAxisRaw("Mouse Y") *
                                    Time.deltaTime * rotVel;

        contRotX = Mathf.Clamp(contRotX, -60, 60);

        Quaternion rotX = Quaternion.AngleAxis(contRotX,
                                               Vector3.left);

        transform.localRotation = rotIni * rotX;
    }
}
