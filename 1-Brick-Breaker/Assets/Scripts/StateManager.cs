using UnityEngine;

/// <summary>
/// Maintains the state of the player's score and lives through level changes.
/// </summary>
public class StateManager : MonoBehaviour
{
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
	/// Make the StateManager persist over scene loads and reset it's values.
	/// </summary>
	void Awake ()
	{
		DontDestroyOnLoad (gameObject);
		Reset ();
	}


	/// <summary>
	/// Reset the lives and score to initial state.
	/// </summary>
	private void Reset ()
	{
		Lives = 3;
		Score = 0;
	}
}
