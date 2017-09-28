using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class YouWinController : MonoBehaviour
{
	public AudioClip winSound;
	private float ySpeed = 0f;
	private float maxY = 21.0f;
	private bool gameIsWon = false;
	private BallController ball;

	void Start ()
	{
		ball = FindObjectOfType<BallController> ();
	}

	void Update ()
	{
		if (gameIsWon && Input.GetButtonDown ("Launch"))
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
		ball.Explode ();
		gameIsWon = true;
		AudioSource.PlayClipAtPoint (winSound, transform.position);
		ySpeed = 5f;
	}
}
