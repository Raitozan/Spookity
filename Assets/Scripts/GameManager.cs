using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

	public int player1Victory;
	public int player2Victory;

	public Text p1v;
	public Text p2v;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			player1Victory = 0;
			player2Victory = 0;
		}
		else if (instance != this)
			Destroy(gameObject);

		DontDestroyOnLoad(this);
	}

	// Update is called once per frame
	void Update () {
		p1v.text = player1Victory.ToString() + " /";
		p2v.text = "/ " + player2Victory.ToString();
	}
}
