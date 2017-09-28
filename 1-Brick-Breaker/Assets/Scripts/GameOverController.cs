using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameOverController : MonoBehaviour
{
	public AudioClip loseSound;
	private float ySpeed = 0f;
	private float maxY = 20.0f;
	private bool gameIsOver = false;


	void Update ()
	{
		if (gameIsOver && Input.GetButtonDown ("Launch"))
			SceneManager.LoadScene ("Game_Level_01");

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
		AudioSource.PlayClipAtPoint (loseSound, transform.position);
		ySpeed = 5f;
	}
}
