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
		float x = 0.0f, z = 0.0f;
		if (gameObject.CompareTag("Player1"))
		{
			x = Input.GetAxis("Horizontal");
			z = Input.GetAxis("Vertical");
		}
		else if (gameObject.CompareTag("Player2"))
		{
			x = Input.GetAxis("Horizontal2");
			z = Input.GetAxis("Vertical2");
		}

        transform.Translate(x * speed * Time.deltaTime, 0, z * speed * Time.deltaTime);
    }

    private void LeftArm()
    {
        if ((Input.GetButtonDown("LeftArm") && CompareTag("Player1")) || (Input.GetButtonDown("LeftArm2") && CompareTag("Player2")))
		{
            Debug.Log("Lancer bras gauche");
        }
    }

    private void RightArm()
    {
        if ((Input.GetButtonDown("RightArm") && CompareTag("Player1")) || (Input.GetButtonDown("RightArm2") && CompareTag("Player2")))
        {
            Debug.Log("Lancer bras droit");
        }
    }

    private void LeftLeg()
    {
        if ((Input.GetAxis("LeftLeg") >= 0.75 && CompareTag("Player1")) || (Input.GetAxis("LeftLeg2") >= 0.75 && CompareTag("Player2")))
        {
            Debug.Log("Lancer jambe gauche");
        }
    }

    private void RightLeg()
    {
        if ((Input.GetAxis("RightLeg") >= 0.75 && CompareTag("Player1")) || (Input.GetAxis("RightLeg2") >= 0.75 && CompareTag("Player2")))
		{
            Debug.Log("Lancer jambe droite");
        }
    }

    private void Head()
    {
        if ((Input.GetButtonDown("Head") && CompareTag("Player1")) || (Input.GetButtonDown("Head2") && CompareTag("Player2")))
		{
            Debug.Log("Lancer tête");
        }
        
    }
}
