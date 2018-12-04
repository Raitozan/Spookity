using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	
	public int health = 100;
    private float speed = 15.0f;
	public bool team1;

	[Header("Members")]
	public GameObject head;
	public GameObject leftArm;
	public GameObject rightArm;
	public GameObject leftLeg;
	public GameObject rightLeg;

	[Header("Thrown Members")]
	public GameObject thrownHead;
	public GameObject thrownArm;
	public GameObject thrownLeg;

	public Animator animator;

	void Start()
    {
		if (gameObject.CompareTag("Player1"))
			team1 = true;
		else
			team1 = false;

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

		if (health <= 0)
			Debug.Log("DED");
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
		if ((Input.GetKeyDown(KeyCode.Joystick1Button0) && CompareTag("Player1")) || (Input.GetKeyDown(KeyCode.Joystick2Button0) && CompareTag("Player2")))
		{
			if (leftArm.activeInHierarchy || rightArm.activeInHierarchy)
			{
				animator.SetBool("isAttacking", true);
			}

		}
	}

    private void LeftArm()
    {
        if ((Input.GetKeyDown(KeyCode.Joystick1Button4) && team1 && leftArm.activeInHierarchy) || (Input.GetKeyDown(KeyCode.Joystick2Button4) && !team1 && leftArm.activeInHierarchy))
		{
			leftArm.SetActive(false);
			ThrownMember leftA = Instantiate(thrownArm, leftArm.transform.position, Quaternion.identity).GetComponent<ThrownMember>();
			leftA.direction = transform.forward;
			leftA.start = transform.position;
			leftA.damage = 10;

			if (team1)
				leftA.team1 = true;
			else
				leftA.team1 = false;
		}
    }

    private void RightArm()
    {
        if ((Input.GetKeyDown(KeyCode.Joystick1Button5) && team1 && rightArm.activeInHierarchy) || (Input.GetKeyDown(KeyCode.Joystick2Button5) && !team1 && rightArm.activeInHierarchy))
        {
			rightArm.SetActive(false);
			animator.SetBool("baseAttackRight", false);
			ThrownMember rightA = Instantiate(thrownArm, rightArm.transform.position, Quaternion.identity).GetComponent<ThrownMember>();
			rightA.direction = transform.forward;
			rightA.start = transform.position;
			rightA.damage = 15;

			if (team1)
				rightA.team1 = true;
			else
				rightA.team1 = false;
		}
    }

    private void LeftLeg()
    {
        if ((Input.GetAxis("LeftLeg") >= 0.75 && team1 && leftLeg.activeInHierarchy) || (Input.GetAxis("LeftLeg2") >= 0.75 && !team1 && leftLeg.activeInHierarchy))
        {
			leftLeg.SetActive(false);
			ThrownMember leftL = Instantiate(thrownLeg, leftLeg.transform.position, Quaternion.identity).GetComponent<ThrownMember>();
			leftL.direction = transform.forward;
			leftL.start = transform.position;
			leftL.damage = 10;

			if (team1)
				leftL.team1 = true;
			else
				leftL.team1 = false;
		}
    }

    private void RightLeg()
    {
        if ((Input.GetAxis("RightLeg") >= 0.75 && team1 && rightLeg.activeInHierarchy) || (Input.GetAxis("RightLeg2") >= 0.75 && !team1 && rightLeg.activeInHierarchy))
		{
			rightLeg.SetActive(false);
			ThrownMember rightL = Instantiate(thrownLeg, rightLeg.transform.position, Quaternion.identity).GetComponent<ThrownMember>();
			rightL.direction = transform.forward;
			rightL.start = transform.position;
			rightL.damage = 10;

			if (team1)
				rightL.team1 = true;
			else
				rightL.team1 = false;
		}
    }

    private void Head()
    {
        if ((Input.GetKeyDown(KeyCode.Joystick1Button3) && team1 && head.activeInHierarchy) || (Input.GetKeyDown(KeyCode.Joystick2Button3) && !team1 && head.activeInHierarchy))
		{
			head.SetActive(false);
			ThrownMember tHead = Instantiate(thrownHead, head.transform.position, Quaternion.identity).GetComponent<ThrownMember>();
			tHead.direction = transform.forward;
			tHead.start = transform.position;
			tHead.damage = 30;

			if (team1)
				tHead.team1 = true;
			else
				tHead.team1 = false;
		}
    }
}
