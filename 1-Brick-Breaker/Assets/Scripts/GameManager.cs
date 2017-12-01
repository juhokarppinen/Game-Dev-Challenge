using UnityEngine;
using System.Collections;

/// <summary>
/// A facade / messenger class for general bookkeeping and managing various object interactions.
/// </summary>
public class GameManager : MonoBehaviour
{
	private static BallController ball;
	private static PaddleController paddle;
	private static LivesController lives;
	private static ScoreController score;
	private static GameOverController gameOver;
	private static YouWinController win;


	void Start ()
	{
		ball = FindObjectOfType<BallController> ();
		paddle = FindObjectOfType<PaddleController> ();
		lives = FindObjectOfType<LivesController> ();
		score = FindObjectOfType<ScoreController> ();
		gameOver = FindObjectOfType<GameOverController> ();
		win = FindObjectOfType<YouWinController> ();
	}


	public static void BallLost (Vector3 position)
	{
		SoundManager.Play (SoundManager.Sound.BallLost, position);
		lives.Decrease ();
		paddle.MakeNormal ();
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
		score.AddPoints (scoreValue);
	}


	public static void Win ()
	{
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
