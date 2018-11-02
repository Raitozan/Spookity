using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

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
        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");

        transform.Translate(x * speed * Time.deltaTime, 0, z * speed * Time.deltaTime);
    }

    private void LeftArm()
    {
        if (Input.GetButtonDown("LeftArm"))
        {
            Debug.Log("Lancer bras gauche");
        }
    }

    private void RightArm()
    {
        if (Input.GetButtonDown("RightArm"))
        {
            Debug.Log("Lancer bras droit");
        }
    }

    private void LeftLeg()
    {
        if (Input.GetAxis("LeftLeg") >= 0.75)
        {
            Debug.Log("Lancer jambe gauche");
        }
    }

    private void RightLeg()
    {
        if (Input.GetAxis("RightLeg") >= 0.75)
        {
            Debug.Log("Lancer jambe droite");
        }
    }

    private void Head()
    {
        if (Input.GetButtonDown("Head"))
        {
            Debug.Log("Lancer tête");
        }
    }
}
