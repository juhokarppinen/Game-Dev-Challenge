using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour
{
	public Text scoreText;

	private int score = 0;

	void Awake ()
	{
		scoreText = GetComponent<Text> ();
	}

	public int getScore ()
	{
		return score;
	}

	public void Add (int amount)
	{
		Assert.IsTrue (amount >= 0);
		score += amount;
		UpdateText ();
	}

	public void Substract (int amount)
	{
		Assert.IsTrue (amount >= 0);
		score -= amount;
		UpdateText ();
	}

	private void UpdateText ()
	{
		scoreText.text = score.ToString ();
	}
}
