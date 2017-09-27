using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreController : MonoBehaviour
{
	private int score = 0;
	private Text scoreText;
	private YouWinController win;

	void Start ()
	{
		scoreText = GetComponent<Text> ();
		win = FindObjectOfType<YouWinController> ();
		UpdateText ();
	}

	public void AddPoints (int points)
	{
		score += points;
		UpdateText ();
	}

	private void UpdateText ()
	{
		scoreText.text = "SCORE: " + score.ToString ();
	}


	public void Win ()
	{
		win.Lift ();
	}
}
