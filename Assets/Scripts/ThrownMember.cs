using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownMember: MonoBehaviour {

	private AudioSource audioSource;

	public AudioClip nathanHurt;
	public AudioClip nathanHurt2;
	public AudioClip adrienHurt;
	public AudioClip adrienHurt2;

	public enum BodyPart { LeftArm, RightArm, LeftLeg, RightLeg, Head };

	public Transform player;
	public BodyPart part;
	public float getBackSpeed;

	public float speed;
	public float rotateSpeed;
	public Vector3 direction;
	public Vector3 start;
	public float maxDist;
	public bool onGround;

	public bool team1;
	public int damage;


	// Use this for initialization
	void Start () {
		onGround = false;
		audioSource = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!onGround)
		{
			Rotate();
			transform.position += direction * speed * Time.deltaTime;
			if ((transform.position - start).magnitude >= maxDist)
				PutOnGround();
		}
		else
		{
			switch (part)
			{
				case BodyPart.LeftArm:
					if ((Input.GetKeyDown(KeyCode.Joystick1Button4) && team1) || (Input.GetKeyDown(KeyCode.Joystick2Button4) && !team1))
						GetBackToPlayer();
					break;
				case BodyPart.RightArm:
					if ((Input.GetKeyDown(KeyCode.Joystick1Button5) && team1) || (Input.GetKeyDown(KeyCode.Joystick2Button5) && !team1))
						GetBackToPlayer();
					break;
				case BodyPart.LeftLeg:
					if ((Input.GetAxis("LeftLeg") >= 0.75 && team1) || (Input.GetAxis("LeftLeg2") >= 0.75 && !team1))
						GetBackToPlayer();
					break;
				case BodyPart.RightLeg:
					if ((Input.GetAxis("RightLeg") >= 0.75 && team1) || (Input.GetAxis("RightLeg2") >= 0.75 && !team1))
						GetBackToPlayer();
					break;
				case BodyPart.Head:
					if ((Input.GetKeyDown(KeyCode.Joystick1Button3) && team1) || (Input.GetKeyDown(KeyCode.Joystick2Button3) && !team1))
						GetBackToPlayer();
					break;
			}
		}
	}

	private void Rotate()
	{
		if(part != BodyPart.LeftLeg && part != BodyPart.RightLeg)
			transform.Rotate(new Vector3(0, 0, -rotateSpeed*Time.deltaTime));
		else
			transform.Rotate(new Vector3(0, rotateSpeed * Time.deltaTime, 0));
	}

	private void PutOnGround()
	{
		if (part == BodyPart.LeftArm || part == BodyPart.RightArm)
		{
			transform.position = new Vector3(transform.position.x, 1.0f, transform.position.z);
			transform.localRotation = Quaternion.Euler(new Vector3(90.0f, transform.localRotation.y, transform.localRotation.z));
		}
		else
		{
			transform.position = new Vector3(transform.position.x, 1.05f, transform.position.z);
			if (part == BodyPart.Head)
				transform.localRotation = Quaternion.Euler(new Vector3(transform.localRotation.x, transform.localRotation.y, 90.0f));
		}
		onGround = true;
	}

	private void GetBackToPlayer()
	{
		Vector3 direction = player.position - transform.position;
		direction.y = 0;
		transform.position += direction.normalized * getBackSpeed;
	}

	private void GiveBackMember(PlayerController player)
	{
		switch (part)
		{
			case BodyPart.LeftArm:
				player.SpeedupReload();
				player.leftArm.SetActive(true);
				break;
			case BodyPart.RightArm:
				player.SpeedupReload();
				player.rightArm.SetActive(true);
				player.animator.SetBool("baseAttackRight", true);
				break;
			case BodyPart.LeftLeg:
				player.SpeedupSpeed();
				player.leftLeg.SetActive(true);
				if (!player.rightLeg.activeInHierarchy)
					player.GetBackOnLeg();
				break;
			case BodyPart.RightLeg:
				player.SpeedupSpeed();
				player.rightLeg.SetActive(true);
				if (!player.leftLeg.activeInHierarchy)
					player.GetBackOnLeg();
				break;
			case BodyPart.Head:
				player.NormalControls();
				player.head.SetActive(true);
				break;
		}

		Destroy(gameObject);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (((!team1 && other.CompareTag("Player1")) || (team1 && other.CompareTag("Player2"))) && !onGround)
		{
			if (other.CompareTag("Player1"))
			{
				if (Random.Range(0, 2) == 0)
					audioSource.PlayOneShot(nathanHurt);
				else
					audioSource.PlayOneShot(nathanHurt2);
			}
			else
			{
				if (Random.Range(0, 2) == 0)
					audioSource.PlayOneShot(adrienHurt);
				else
					audioSource.PlayOneShot(adrienHurt2);
			}

			other.GetComponent<PlayerController>().health -= damage;
			PutOnGround();
		}

		if (other.CompareTag("Wall") && !onGround)
		{
			PutOnGround();
		}

		if (((team1 && other.CompareTag("Player1")) || (!team1 && other.CompareTag("Player2"))) && onGround)
		{
			GiveBackMember(other.GetComponent<PlayerController>());
		}
	}
}
