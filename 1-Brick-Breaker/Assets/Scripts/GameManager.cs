using UnityEngine;
using System.Collections;

/// <summary>
/// A facade / messenger class for general bookkeeping and managing various object interactions.
/// All public methods are static so that they can be called without an explicit object reference.
/// </summary>
public class GameManager : MonoBehaviour
{
	private static StateManager state;
	private static PaddleController paddle;
	private static GameOverController gameOver;
	private static YouWinController win;
	private static Lives lives;
	private static Score score;
	private static ExtraLifeCounter extraLifeCounter;


	void Awake ()
	{
		state = FindObjectOfType<StateManager> ();
		paddle = FindObjectOfType<PaddleController> ();
		gameOver = FindObjectOfType<GameOverController> ();
		win = FindObjectOfType<YouWinController> ();
		lives = GetComponent<Lives> ();
		score = GetComponent<Score> ();
		extraLifeCounter = GetComponent<ExtraLifeCounter> ();
	}


	void Start ()
	{
		GetState ();
	}


	public static void BallLost (bool noBallsInPlay)
	{
		if (noBallsInPlay && lives.Get () <= 0) {
			GameOver ();
		} else {
			paddle.MakeNormal ();
		}
	}


	public static void BallHitTopWall ()
	{
		paddle.MakeSmaller ();
	}


	public static void AddToScore (int scoreValue)
	{
		score.Add (scoreValue);
		if (extraLifeCounter.UpdateCounter (scoreValue)) {
			lives.Add (1);
		}
	}


	public static void SetScore (int scoreValue)
	{
		score.Set (scoreValue);
	}


	public static bool HasLives ()
	{
		return (lives.Get () > 0);
	}


	public static void SetLives (int livesValue)
	{
		lives.Set (livesValue);
	}


	public static void DecrementLives ()
	{
		lives.Remove (1);
	}


	public static void SetOneUpCountTo (int countValue)
	{
		extraLifeCounter.Set (countValue);
	}


	public static void Win ()
	{
		SetState ();
		LevelManager.NextLevel ();

		// Not executed.
		SoundManager.Play (SoundManager.Sound.Win, Vector3.zero);
		win.Lift ();
	}


	public static void GameOver ()
	{
		gameOver.Lift ();
		paddle.Explode ();
	}


	private static void SetState ()
	{
		state.Score = score.Get ();
		state.Lives = lives.Get ();
		state.OneUpCount = extraLifeCounter.Get ();
	}


	private static void GetState ()
	{
		score.Set (state.Score);
		lives.Set (state.Lives);
		extraLifeCounter.Set (state.OneUpCount);
	}
}
