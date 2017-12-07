﻿using UnityEngine;

/// <summary>
/// A global facade / messenger class for managing various object interactions in a centralised fashion.
/// All public methods are static so that they can be called without an explicit object reference.
/// Will instantiate a new StateManager if it doesn't find one.
/// </summary>
public class GameManager : MonoBehaviour
{
	public StateManager stateManager;

	private static StateManager state;
	private static LevelManager level;
	private static PaddleController paddle;
	private static GameOverController gameOver;
	private static Lives lives;
	private static Score score;
	private static ScoreMultiplier scoreMultiplier;
	private static ExtraLifeCounter oneUpCount;


	/// <summary>
	/// Set object references. If a StateManager isn't found, instantiate a new one.
	/// </summary>
	void Awake ()
	{
		state = FindObjectOfType<StateManager> ();
		if (!state) {
			state = Instantiate (stateManager).GetComponent<StateManager> ();
		}

		level = FindObjectOfType<LevelManager> ();
		paddle = FindObjectOfType<PaddleController> ();
		gameOver = FindObjectOfType<GameOverController> ();
		lives = GetComponent<Lives> ();
		score = GetComponent<Score> ();
		scoreMultiplier = GetComponent<ScoreMultiplier> ();
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
		if (noBallsInPlay) {
			scoreMultiplier.Reset ();
			paddle.MakeNormal ();
		}

		if (noBallsInPlay && !HasLives ()) {
			GameOver ();
		}
	}


	public static void AddToScore (int scoreValue)
	{
		int points = scoreValue * scoreMultiplier.Value;
		score.Add (points);
		scoreMultiplier.Add ();
		if (oneUpCount.UpdateCounter (points)) {
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
		lives.Add (BallController.RetrieveBallsInPlay ());
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


	void Update ()
	{
		BrickController someBrick = FindObjectOfType<BrickController> ();
		if (!someBrick) {
			Win ();
		}
	}


	public static void GotPowerUp ()
	{
		BallController latestBall = FindObjectOfType<BallController> ();
		if (latestBall)
			latestBall.ActivatePowerBall ();
	}
}
