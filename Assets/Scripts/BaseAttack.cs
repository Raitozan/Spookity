using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAttack : MonoBehaviour {

	public bool team1;
	public int damage;

	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player1") && !team1 || other.CompareTag("Player2") && team1)
		{
			other.GetComponent<PlayerController>().health -= damage;
		}
	}
}
