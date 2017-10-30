using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour
{
	public Text scoreText;
	private float maxScore = 0f;


	void Awake ()
	{
		scoreText = GetComponent<Text> ();
	}


	public float GetScore ()
	{
		return maxScore;
	}


	public void UpdateText (float score)
	{
		if (score > maxScore)
			maxScore = score;
		scoreText.text = "Distance: " + Mathf.Ceil (maxScore).ToString ();
	}
}
