using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameOverController : MonoBehaviour
{
	private float ySpeed = 0f;
	private float maxY = 18.0f;
	private bool gameIsOver = false;
	private RestartController restart;

	void Start ()
	{
		restart = FindObjectOfType<RestartController> ();
	}

	void Update ()
	{
		if (gameIsOver && Input.GetButtonDown ("Jump"))
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
		Text restartText = restart.gameObject.GetComponent<Text> ();
		restartText.text = "Press Space to Restart";
		gameIsOver = true;
		ySpeed = 5f;
	}
}
