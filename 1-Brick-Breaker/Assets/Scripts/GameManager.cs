using UnityEngine;

/// <summary>
/// A global facade / messenger class for managing various object interactions in a centralised fashion.
/// All public methods are static so that they can be called without an explicit object reference.
/// </summary>
public class GameManager : MonoBehaviour
{
	private static StateManager state;
	private static LevelManager level;
	private static PaddleController paddle;
	private static GameOverController gameOver;
	private static YouWinController win;
	private static Lives lives;
	private static Score score;
	private static ExtraLifeCounter oneUpCount;


	void Awake ()
	{
		state = FindObjectOfType<StateManager> ();
		level = FindObjectOfType<LevelManager> ();
		paddle = FindObjectOfType<PaddleController> ();
		gameOver = FindObjectOfType<GameOverController> ();
		win = FindObjectOfType<YouWinController> ();
		lives = GetComponent<Lives> ();
		score = GetComponent<Score> ();
		oneUpCount = GetComponent<ExtraLifeCounter> ();
	}


	/// <summary>
	/// Load the player's state from StateManager before calculating the first update frame.
	/// </summary>
	void Start ()
	{
		FetchState ();
	}


	public static void BallLost (bool noBallsInPlay)
	{
		if (noBallsInPlay && lives.Get () <= 0) {
			GameOver ();
		} else {
			paddle.MakeNormal ();
		}
	}


	public static void AddToScore (int scoreValue)
	{
		score.Add (scoreValue);
		if (oneUpCount.UpdateCounter (scoreValue)) {
			lives.Add (1);
		}
	}


	public static bool HasLives ()
	{
		return (lives.Get () > 0);
	}


	public static void DecrementLives ()
	{
		lives.Remove (1);
	}

	
	public static void GameOver ()
	{
		gameOver.Lift ();
		paddle.Explode ();
	}


	public static void Win ()
	{
		StoreState ();
		level.NextLevel ();
	}


	public static void LoadLevel (LevelManager.Level lvl)
	{
		level.Load (lvl);
	}


	/// <summary>
	/// Store score, lives and 1upCounter value to the StateManager.
	/// </summary>
	private static void StoreState ()
	{
		state.Score = score.Get ();
		state.Lives = lives.Get ();
		state.OneUpCount = oneUpCount.Get ();
	}


	/// <summary>
	/// Fetch score, lives and 1upCounter value from the StateManager.
	/// </summary>
	private static void FetchState ()
	{
		SetScore (state.Score);
		SetLives (state.Lives);
		SetOneUpCount (state.OneUpCount);
	}

		
	public static void SetScore (int scoreValue)
	{
		score.Set (scoreValue);
	}


	public static void SetLives (int livesValue)
	{
		lives.Set (livesValue);
	}


	public static void SetOneUpCount (int countValue)
	{
		oneUpCount.Set (countValue);
	}
}
