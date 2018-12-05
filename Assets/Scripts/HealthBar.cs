using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

	public PlayerController player;

	public Image healthBar;
	public Text healthText;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		UpdateCanvasInfo();
	}

	public void UpdateCanvasInfo()
	{
		healthBar.fillAmount = (float)(player.health) / 300.0f;
		healthText.text = player.health.ToString() + " / 300";
	}
}
