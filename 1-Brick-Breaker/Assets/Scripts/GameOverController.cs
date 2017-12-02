using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverController : MonoBehaviour
{
	private float ySpeed = 0f;
	private float maxY = 20.0f;
	private bool gameIsOver = false;


	void Update ()
	{
		if (gameIsOver && Input.GetButtonDown ("Launch"))
			LevelManager.Load (LevelManager.Level.L_01);

		float x = transform.position.x;
		float y = transform.position.y;
		float z = transform.position.z;

		if (y < maxY) {
			transform.position = new Vector3 (x, y + ySpeed * Time.deltaTime, z);
		} else {
			ySpeed = 0f;
		}
	}


	public void Lift ()
	{
		gameIsOver = true;
		SoundManager.Play (SoundManager.Sound.Lose, Vector3.zero);
		ySpeed = 5f;
	}
}
