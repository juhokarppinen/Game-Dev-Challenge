using UnityEngine;
using System.Collections;

public class LivesController : MonoBehaviour
{
	public GameObject[] lifeCounters;

	private int lives;
	private PaddleController paddle;

	// Use this for initialization
	void Start ()
	{
		lives = lifeCounters.Length;
		paddle = FindObjectOfType<PaddleController> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
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
		print ("Game over");
		paddle.Explode ();
	}
}
