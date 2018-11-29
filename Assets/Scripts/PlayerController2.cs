using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour {

    private float speed = 15.0f;

    void Start()
    {

    }


    void Update()
    {
        Move();
        LeftArm();
        RightArm();
        LeftLeg();
        RightLeg();
        Head();
    }

    private void Move()
    {
        var x = Input.GetAxis("Horizontal2");
        var z = Input.GetAxis("Vertical2");

        transform.Translate(x * speed * Time.deltaTime, 0, z * speed * Time.deltaTime);
    }

    private void LeftArm()
    {
        if (Input.GetButtonDown("LeftArm2"))
        {
            Debug.Log("Lancer bras gauche 2");
        }
    }

    private void RightArm()
    {
        if (Input.GetButtonDown("RightArm2"))
        {
            Debug.Log("Lancer bras droit 2");
        }
    }

    private void LeftLeg()
    {
        if (Input.GetAxis("LeftLeg2") >= 0.75)
        {
            Debug.Log("Lancer jambe gauche 2");
        }
    }

    private void RightLeg()
    {
        if (Input.GetAxis("RightLeg2") >= 0.75)
        {
            Debug.Log("Lancer jambe droite 2");
        }
    }

    private void Head()
    {
        if (Input.GetButtonDown("Head2"))
        {
            Debug.Log("Lancer tête 2");
        }
    }
}
