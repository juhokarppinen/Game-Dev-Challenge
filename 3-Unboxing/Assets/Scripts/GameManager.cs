using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	Dictionary<string,float> scores;
	TimerText timer;

	public Text levelText;
	public Text bestTimeText;
	public AudioClip[] clips;

	string currentLevel;

	void Start ()
	{
		scores = new Dictionary<string,float> ();	
		timer = FindObjectOfType<TimerText> ();
	}


	public bool SetScore (string level, float score)
	{
		if (!scores.ContainsKey (level)) {
			scores.Add (level, score);
			return true; // New best time.
		}

		if (score < scores [level]) {
			scores [level] = score;
			bestTimeText.text = timer.FormatTime (score);
			AudioSource.PlayClipAtPoint (clips [1], Vector3.zero);
			return true; // Better than previous best time.
		}

		return false; // Wasn't best time.
	}


	public float GetScore (string level)
	{
		if (!scores.ContainsKey (level)) {
			return 6000 - 0.01f; // Default "best" time.
		}

		return scores [level];
	}


	private void UpdateBestTime ()
	{
		bestTimeText.text = "Best:\n" + timer.FormatTime (GetScore (currentLevel));
	}


	public void LevelComplete ()
	{
		if (SetScore (currentLevel, timer.GetTime ())) {
			AudioSource.PlayClipAtPoint (clips [1], Vector3.zero);
			UpdateBestTime ();
		} else {
			AudioSource.PlayClipAtPoint (clips [0], Vector3.zero);
		}
		timer.StopTimer ();
	}


	public void StartLevel (string level)
	{
		AudioSource.PlayClipAtPoint (clips [2], Vector3.zero);
		timer.ResetTimer ();
		timer.StartTimer ();
		currentLevel = level;
		levelText.text = currentLevel;
		UpdateBestTime ();
	}


	public void ClearLevel ()
	{
		timer.StopTimer ();
		currentLevel = "";
		levelText.text = "Show Image Target To Begin";
		bestTimeText.text = "";
	}
}