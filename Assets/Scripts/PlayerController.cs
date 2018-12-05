using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public Animator animator;

	public int health;
	public float baseSpeed;
	public float speed;

	public bool team1;

	public float baseReloadTime;
	public float reloadTime;
	[HideInInspector]
	public float reloadTimer;
	[HideInInspector]
	public bool inverted;

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

	void Start()
    {
		if (gameObject.CompareTag("Player1"))
			team1 = true;
		else
			team1 = false;

		animator.SetBool("baseAttackRight", true);

		speed = baseSpeed;
		reloadTime = baseReloadTime;
		inverted = false;
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
		if (inverted)
		{
			z = -z;
			x = -x;
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
		if(reloadTimer > 0)
			reloadTimer -= Time.deltaTime;
		else if ((Input.GetKeyDown(KeyCode.Joystick1Button0) && CompareTag("Player1")) || (Input.GetKeyDown(KeyCode.Joystick2Button0) && CompareTag("Player2")))
		{
			if (leftArm.activeInHierarchy || rightArm.activeInHierarchy)
			{
				animator.SetBool("isAttacking", true);
				reloadTimer = reloadTime;
			}

		}
	}

    private void LeftArm()
    {
        if ((Input.GetKeyDown(KeyCode.Joystick1Button4) && team1 && leftArm.activeInHierarchy) || (Input.GetKeyDown(KeyCode.Joystick2Button4) && !team1 && leftArm.activeInHierarchy))
		{
			leftArm.SetActive(false);
			slowdownReload();
			ThrownMember leftA = Instantiate(thrownArm, leftArm.transform.position, Quaternion.identity).GetComponent<ThrownMember>();
			leftA.player = transform;
			leftA.part = ThrownMember.BodyPart.LeftArm;
			leftA.direction = transform.forward;
			leftA.start = transform.position;
			if (rightArm.activeInHierarchy)
				leftA.damage = 10;
			else
				leftA.damage = 15;

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
			slowdownReload();
			animator.SetBool("baseAttackRight", false);
			ThrownMember rightA = Instantiate(thrownArm, rightArm.transform.position, Quaternion.identity).GetComponent<ThrownMember>();
			rightA.player = transform;
			rightA.part = ThrownMember.BodyPart.RightArm;
			rightA.direction = transform.forward;
			rightA.start = transform.position;
			if (leftArm.activeInHierarchy)
				rightA.damage = 10;
			else
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
			slowdownSpeed();
			ThrownMember leftL = Instantiate(thrownLeg, leftLeg.transform.position, Quaternion.identity).GetComponent<ThrownMember>();
			leftL.player = transform;
			leftL.part = ThrownMember.BodyPart.LeftLeg;
			leftL.direction = transform.forward;
			leftL.start = transform.position;
			if (rightLeg.activeInHierarchy)
				leftL.damage = 20;
			else
			{
				leftL.damage = 25;
				PutOnGround();
			}

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
			slowdownSpeed();
			ThrownMember rightL = Instantiate(thrownLeg, rightLeg.transform.position, Quaternion.identity).GetComponent<ThrownMember>();
			rightL.player = transform;
			rightL.part = ThrownMember.BodyPart.RightLeg;
			rightL.direction = transform.forward;
			rightL.start = transform.position;
			if (leftLeg.activeInHierarchy)
				rightL.damage = 20;
			else
			{
				rightL.damage = 25;
				PutOnGround();
			}

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
			invertControls();
			ThrownMember tHead = Instantiate(thrownHead, head.transform.position, Quaternion.identity).GetComponent<ThrownMember>();
			tHead.player = transform;
			tHead.part = ThrownMember.BodyPart.Head;
			tHead.direction = transform.forward;
			tHead.start = transform.position;
			tHead.damage = 30;

			if (team1)
				tHead.team1 = true;
			else
				tHead.team1 = false;
		}
    }

	public void slowdownSpeed()
	{
		speed -= 0.3f * baseSpeed;
	}

	public void speedupSpeed()
	{
		speed += 0.3f * baseSpeed;
	}

	public void slowdownReload()
	{
		reloadTime += baseReloadTime;
	}

	public void speedupReload()
	{
		reloadTime -= baseReloadTime;
	}

	public void invertControls()
	{
		inverted = true;
	}

	public void normalControls()
	{
		inverted = false;
	}

	public void PutOnGround()
	{
		transform.position = new Vector3(transform.position.x, 1.6f, transform.position.z);
	}

	public void GetBackOnLeg()
	{
		transform.position = new Vector3(transform.position.x, 2.75f, transform.position.z);
	}
}
