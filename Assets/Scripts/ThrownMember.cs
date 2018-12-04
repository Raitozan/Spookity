using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownMember: MonoBehaviour {

	public bool leg;
	public bool head;
	public float speed;
	public float rotateSpeed;
	public Vector3 direction;
	public Vector3 start;
	public float maxDist;
	public bool onGround;
	public string buttonName;

	public bool team1;
	public int damage;


	// Use this for initialization
	void Start () {
		onGround = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!onGround)
		{
			Rotate();
			transform.position += direction * speed * Time.deltaTime;
			if ((transform.position - start).magnitude >= maxDist)
			{
				if (!leg && !head)
				{
					transform.position = new Vector3(transform.position.x, 1.0f, transform.position.z);
					transform.localRotation = Quaternion.Euler(new Vector3(90.0f, transform.localRotation.y, transform.localRotation.z));
				}
				else
				{
					transform.position = new Vector3(transform.position.x, 1.05f, transform.position.z);
					if(head)
						transform.localRotation = Quaternion.Euler(new Vector3(transform.localRotation.x, transform.localRotation.y, 90.0f));
				}
				onGround = true;
			}
		}
	}

	public void Rotate()
	{
		if(!leg)
			transform.Rotate(new Vector3(0, 0, -rotateSpeed*Time.deltaTime));
		else
			transform.Rotate(new Vector3(0, rotateSpeed * Time.deltaTime, 0));
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!team1 && other.CompareTag("Player1") || team1 && other.CompareTag("Player2"))
		{
			other.GetComponent<PlayerController>().health -= damage;

			if (!leg && !head)
			{
				transform.position = new Vector3(transform.position.x, 1.0f, transform.position.z);
				transform.localRotation = Quaternion.Euler(new Vector3(90.0f, transform.localRotation.y, transform.localRotation.z));
			}
			else
			{
				transform.position = new Vector3(transform.position.x, 1.05f, transform.position.z);
				if (head)
					transform.localRotation = Quaternion.Euler(new Vector3(transform.localRotation.x, transform.localRotation.y, 90.0f));
			}
			onGround = true;
		}

		if (other.CompareTag("Wall"))
		{
			if (!leg && !head)
			{
				transform.position = new Vector3(transform.position.x, 1.0f, transform.position.z);
				transform.localRotation = Quaternion.Euler(new Vector3(90.0f, transform.localRotation.y, transform.localRotation.z));
			}
			else
			{
				transform.position = new Vector3(transform.position.x, 1.05f, transform.position.z);
				if (head)
					transform.localRotation = Quaternion.Euler(new Vector3(transform.localRotation.x, transform.localRotation.y, 90.0f));
			}
			onGround = true;
		}
	}
}
