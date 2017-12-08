using UnityEngine;

public class YouWinController : MonoBehaviour
{
	private float ySpeed = 0f;
	private float maxY = 21.0f;
	private bool gameIsWon = false;
	private InputManager input;


	void Awake ()
	{
		input = GetComponent<InputManager> ();
	}


	void Update ()
	{
		if (gameIsWon && input.LaunchButtonDown)
			FindObjectOfType<GameManager> ().LoadLevel (LevelManager.Level.GAMEWIN);

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
