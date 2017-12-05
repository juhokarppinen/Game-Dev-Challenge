using UnityEngine;
using System.Collections;

/// <summary>
/// A facade / messenger class for general bookkeeping and managing various object interactions.
/// All public methods are static so that they can be called without an explicit object reference.
/// </summary>
public class GameManager : MonoBehaviour
{
	private static StateManager state;
	private static BallController ball;
	private static PaddleController paddle;
	private static GameOverController gameOver;
	private static YouWinController win;
	private static Lives lives;
	private static Score score;
	private static ExtraLifeCounter extraLifeCounter;


	void Awake ()
	{
		state = FindObjectOfType<StateManager> ();
		ball = FindObjectOfType<BallController> ();
		paddle = FindObjectOfType<PaddleController> ();
		gameOver = FindObjectOfType<GameOverController> ();
		win = FindObjectOfType<YouWinController> ();
		lives = GetComponent<Lives> ();
		score = GetComponent<Score> ();
		extraLifeCounter = GetComponent<ExtraLifeCounter> ();
	}


	void Start ()
	{
		score.Set (state.Score);
		lives.Set (state.Lives);
		extraLifeCounter.Set (state.OneUpCount);
	}


	public static void BallLost (Vector3 position)
	{
		if (lives.Get () > 0) {
			lives.Remove (1);
			paddle.MakeNormal ();
		} else {
			GameOver ();
		}
	}


	public static void BallHitTopWall ()
	{
		paddle.MakeSmaller ();
	}


	public static float GetPaddleX ()
	{
		return paddle.X;
	}


	public static float GetBallX ()
	{
		return ball.transform.position.x;
	}


	public static void AddToScore (int scoreValue)
	{
		score.Add (scoreValue);
		if (extraLifeCounter.UpdateCounter (scoreValue)) {
			lives.Add (1);
		}
	}


	public static void SetScoreTo (int scoreValue)
	{
		score.Set (scoreValue);
	}


	public static void SetLivesTo (int livesValue)
	{
		lives.Set (livesValue);
	}


	public static void SetOneUpCountTo (int countValue)
	{
		extraLifeCounter.Set (countValue);
	}


	public static void Win ()
	{
		state.Score = score.Get ();
		state.Lives = lives.Get ();
		state.OneUpCount = extraLifeCounter.Get ();

		LevelManager.NextLevel ();

		// Not executed.
		SoundManager.Play (SoundManager.Sound.Win, Vector3.zero);
		ball.Explode ();
		win.Lift ();
	}


	public static void GameOver ()
	{
		ball.ShouldBeReplaced = false;
		gameOver.Lift ();
		paddle.Explode ();
	}
}
