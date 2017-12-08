using UnityEngine;

public class Score : MonoBehaviour
{
	private ScoreText scoreText;
	private int score;

	void Awake ()
	{
		scoreText = FindObjectOfType<ScoreText> ();
	}


	public void Add (int amount)
	{
		score += amount;
		scoreText.UpdateText (score);
	}


	public void Set (int amount)
	{
		score = amount;
		scoreText.UpdateText (score);
	}


	public int Get ()
	{
		return score;
	}
}
