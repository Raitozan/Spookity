using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAttack : MonoBehaviour {

	private AudioSource audioSource;

	public AudioClip nathanHurt;
	public AudioClip nathanHurt2;
	public AudioClip adrienHurt;
	public AudioClip adrienHurt2;

	public bool team1;
	public int damage;

	private void Start()
	{
		audioSource = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSource>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player1") && !team1 || other.CompareTag("Player2") && team1)
		{
			other.GetComponent<PlayerController>().health -= damage;

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
		}
	}
}
