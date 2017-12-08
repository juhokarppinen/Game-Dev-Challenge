using UnityEngine;
using UnityEngine.UI;

public class GameOverController : GameManagerCollaborator
{
	private float ySpeed = 0f;
	private float maxY = 20.0f;
	private bool gameIsOver = false;
	private InputManager input;


	void Awake ()
	{
		input = GetComponent<InputManager> ();
	}


	void Update ()
	{
		if (gameIsOver && input.LaunchButtonDown)
			gameManager.LoadLevel (LevelManager.Level.GAMEOVER);

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
