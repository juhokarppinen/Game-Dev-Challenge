using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public static GameManager instance = null;

	private Player player;
	private Vector3 lastCheckpoint;
	private Score score;
	private Lives lives;
	private Camera cam;
	private Vector3 cameraOffset;


	void Awake ()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);
	}


	void Start ()
	{
		player = FindObjectOfType<Player> ();
		lastCheckpoint = new Vector3 (0, 0, 0);
		score = FindObjectOfType<Score> ();
		lives = FindObjectOfType<Lives> ();
		cam = FindObjectOfType<Camera> ();
		cameraOffset = cam.transform.position;
	}


	public void FollowCamera ()
	{
		cam.transform.position = player.transform.position + cameraOffset;
	}


	public void PassedObstacle (Aperture obstacle)
	{
		score.Add (1);
		lastCheckpoint = obstacle.transform.position;
	}


	public void HitObstacle ()
	{
		lives.Substract (1);
		if (lives.getLives () <= 0) {
			GameOver ();
			return;
		}
		ResetToCheckpoint ();
	}


	private void ResetToCheckpoint ()
	{
		player.SetPosition (lastCheckpoint);
	}


	public void GameOver ()
	{
		Debug.Log ("Game Over");
		player.Die ();
	}
}
