using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndManager : MonoBehaviour
{
	public Text victory;

	void Awake()
	{
		Cursor.lockState = CursorLockMode.None;
		if(GameManager.instance.player1Victory > GameManager.instance.player2Victory)
			victory.text = "Player 1 has Won !";
		else
			victory.text = "Player 2 has Won !";
		Destroy(GameManager.instance);
	}

	public void MainMenu()
	{
		Destroy(Music.instance.gameObject);
		SceneManager.LoadScene(0);
	}
	public void Quit()
	{
		Application.Quit();
	}
}
