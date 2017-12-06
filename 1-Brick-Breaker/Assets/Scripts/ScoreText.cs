using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
	public void UpdateText (int score)
	{
		GetComponent<Text> ().text = "SCORE: " + score;
	}
}
