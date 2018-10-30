using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Camera : MonoBehaviour
{

    private float mouseX;
    public Transform playerCam, character, centerPoint;

    // Use this for initialization
    void Start()
    {

    }

    void Update()
    {
        mouseX += Input.GetAxis("Mouse X");
        playerCam.LookAt(centerPoint);
        centerPoint.localRotation = Quaternion.Euler(0, mouseX * 10, 0);

    }

}