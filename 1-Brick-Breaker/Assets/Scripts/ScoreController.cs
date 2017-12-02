using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreController : MonoBehaviour
{
	private int score = 0;
	private Text scoreText;

	void Start ()
	{
		scoreText = GetComponent<Text> ();
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
	}
}
