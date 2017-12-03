using UnityEngine;
using System.Collections;

/// <summary>
/// A facade / messenger class for general bookkeeping and managing various object interactions.
/// All public methods are static so that they can be called without an explicit object reference.
/// </summary>
public class GameManager : MonoBehaviour
{
	private static BallController ball;
	private static PaddleController paddle;
	private static GameOverController gameOver;
	private static YouWinController win;
	private static Lives lives;
	private static Score score;


	void Start ()
	{
		ball = FindObjectOfType<BallController> ();
		paddle = FindObjectOfType<PaddleController> ();
		gameOver = FindObjectOfType<GameOverController> ();
		win = FindObjectOfType<YouWinController> ();
		lives = GetComponent<Lives> ();
		score = GetComponent<Score> ();
	}


	public static void BallLost (Vector3 position)
	{
		lives.Decrement ();
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
		score.Add (scoreValue);
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
