using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private float speed = 15.0f;

	public GameObject head;
	public GameObject leftArm;
	public GameObject rightArm;
	public GameObject leftLeg;
	public GameObject rightLeg;

	public Animator animator;

	void Start()
    {
		animator.SetBool("baseAttackRight", true);
	}


    void Update()
    {
        Move();
		BaseAttack();
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
			z = Input.GetAxis("Horizontal");
			x = Input.GetAxis("Vertical");
		}
		else if (gameObject.CompareTag("Player2"))
		{
			z = Input.GetAxis("Horizontal2");
			x = Input.GetAxis("Vertical2");
		}

		if (x == 0 && z == 0)
		{
			animator.SetBool("isMoving", false);
		}
		else
		{
			animator.SetBool("isMoving", true);
		}

        //transform.Translate(x * speed * Time.deltaTime, 0, z * speed * Time.deltaTime);
		GetComponent<Rigidbody>().MovePosition(transform.position + transform.forward * (x * speed * Time.deltaTime) + transform.right * (z * speed * Time.deltaTime));
    }

	private void BaseAttack()
	{
		animator.SetBool("isAttacking", false);
		if ((Input.GetButtonDown("BaseAttack") && CompareTag("Player1")) || (Input.GetButtonDown("BaseAttack2") && CompareTag("Player2")))
		{
			Debug.Log(Input.GetButtonDown("BaseAttack"));
			Debug.Log(CompareTag("Player1"));
			Debug.Log(Input.GetButtonDown("BaseAttack2"));
			Debug.Log(CompareTag("Player2"));
			if (leftArm.activeInHierarchy || rightArm.activeInHierarchy)
			{
				animator.SetBool("isAttacking", true);
			}

		}
	}

    private void LeftArm()
    {
        if ((Input.GetButtonDown("LeftArm") && CompareTag("Player1")) || (Input.GetButtonDown("LeftArm2") && CompareTag("Player2")))
		{
			leftArm.SetActive(false);
        }
    }

    private void RightArm()
    {
        if ((Input.GetButtonDown("RightArm") && CompareTag("Player1")) || (Input.GetButtonDown("RightArm2") && CompareTag("Player2")))
        {
            Debug.Log("Lancer bras droit");
			animator.SetBool("baseAttackRight", false);
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
