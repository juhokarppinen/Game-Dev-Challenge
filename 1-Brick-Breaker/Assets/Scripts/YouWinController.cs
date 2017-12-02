using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class YouWinController : MonoBehaviour
{
	private float ySpeed = 0f;
	private float maxY = 21.0f;
	private bool gameIsWon = false;


	void Update ()
	{
		if (gameIsWon && Input.GetButtonDown ("Launch"))
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
		gameIsWon = true;
		ySpeed = 5f;
	}
}
