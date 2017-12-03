using UnityEngine;
using System.Collections;

public class LivesController : MonoBehaviour
{
	public GameObject[] lifeCounters;

	private int lives;

	
	void Start ()
	{
		lives = lifeCounters.Length;
	}


	public void Decrease ()
	{
		lives -= 1;
		if (lives >= 0) {
			LifeCounter lifeCounter = lifeCounters [lives].GetComponent<LifeCounter> ();
			lifeCounter.Launch ();
		} else {
			GameManager.GameOver ();
		}
	}
}
