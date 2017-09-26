using UnityEngine;
using System.Collections;

public class LivesController : MonoBehaviour
{
	public GameObject[] lifeCounters;
	private GameOverController gameOver;

	private int lives;
	private PaddleController paddle;

	
	void Start ()
	{
		lives = lifeCounters.Length;
		paddle = FindObjectOfType<PaddleController> ();
		gameOver = FindObjectOfType<GameOverController> ();
	}

	


	public void Decrease ()
	{
		lives -= 1;
		if (lives >= 0) {
			LifeCounterController lifeCounter = lifeCounters [lives].GetComponent<LifeCounterController> ();
			lifeCounter.Launch ();
		} else {
			GameOver ();
		}
	}

	private void GameOver ()
	{
		gameOver.Lift ();
		paddle.Explode ();
	}
}
