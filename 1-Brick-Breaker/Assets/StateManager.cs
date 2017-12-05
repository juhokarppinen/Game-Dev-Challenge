using UnityEngine;
using System.Collections;

/// <summary>
/// Maintains the state of player's score and lives through level changes. Utilises the Singleton pattern.
/// </summary>
public class StateManager : MonoBehaviour
{
	public static StateManager instance;
	private int lives;
	private int score;
	private int oneUpCount;

	public int Lives {
		get { return lives; }
		set { lives = value; }
	}

	public int Score {
		get { return score; }
		set { score = value; }
	}

	public int OneUpCount {
		get { return oneUpCount; }
		set { oneUpCount = value; }
	}


	/// <summary>
	/// Create the singleton instance.
	/// </summary>
	void Awake ()
	{
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			GameManager.SetScoreTo (Score);
			GameManager.SetLivesTo (Lives);
			Destroy (gameObject);
		}
		DontDestroyOnLoad (gameObject);

		lives = 3;
	}
}
