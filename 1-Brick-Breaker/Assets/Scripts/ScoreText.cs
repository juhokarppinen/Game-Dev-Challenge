using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreText : MonoBehaviour
{
	public void UpdateText (int score)
	{
		GetComponent<Text> ().text = "SCORE: " + score;
	}
}
