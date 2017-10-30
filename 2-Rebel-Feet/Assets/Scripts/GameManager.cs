using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
	// public static GameManager instance = null;

	private MusicMatInput input;
	private GameOverText gameOverText;
	private Panel panel;
	private LevelGenerator levelGenerator;
	private Tube outerWall;
	private Player player;
	private Score score;
	private Lives lives;
	private Camera cam;
	private Vector3 lastCheckpoint;
	private Vector3 cameraOffset;

	private bool gameOver = false;
	private bool endScreenLoaded = false;
	private bool cameraInMotion = false;

	public bool GameIsOver {
		get { return gameOver; }
	}


	public bool EndScreenLoaded {
		set { endScreenLoaded = value; }
	}


	public Vector3 PlayerPos {
		get {
			if (player)
				return player.transform.position;
			else
				return Vector3.zero;
		}
	}


	//	void Awake ()
	//	{
	//		if (instance == null)
	//			instance = this;
	//		else if (instance != this)
	//			Destroy (gameObject);
	//
	//		DontDestroyOnLoad (gameObject);
	//	}


	void Start ()
	{
		input = GetComponent<MusicMatInput> ();
		gameOverText = FindObjectOfType<GameOverText> ();
		panel = FindObjectOfType<Panel> ();
		levelGenerator = FindObjectOfType<LevelGenerator> ();
		outerWall = FindObjectOfType<Tube> ();
		player = FindObjectOfType<Player> ();
		lastCheckpoint = Vector3.zero;
		score = FindObjectOfType<Score> ();
		lives = FindObjectOfType<Lives> ();
		cam = FindObjectOfType<Camera> ();
		cameraOffset = cam.transform.position;
	}


	void Update ()
	{
		if (player) {
			MoveOuterWallToPlayer ();
			score.UpdateText (PlayerPos.z);
			levelGenerator.AdjustDifficulty (score.GetScore ());
		}

		if (gameOver && endScreenLoaded && input.InputShield ()) {
			SceneManager.LoadScene ("Main Game");
		}
	}


	void LateUpdate ()
	{
		if (!cameraInMotion && player)
			FollowCamera ();
	}


	void MoveOuterWallToPlayer ()
	{
		outerWall.transform.position = new Vector3 (
			outerWall.transform.position.x, 
			outerWall.transform.position.y, 
			PlayerPos.z);
	}


	private void FollowCamera ()
	{
		Vector3 newPos = Vector3.Slerp (cam.transform.position, PlayerPos + cameraOffset, Time.deltaTime * player.Speed / 4);
		float x = newPos.x;
		float y = newPos.y;
		float z = (PlayerPos + cameraOffset).z;
		cam.transform.position = new Vector3 (x, y, z);
	}


	public void PlayerPassedObstacle (Aperture aperture)
	{
		lastCheckpoint = aperture.transform.position;
		levelGenerator.PopWall (aperture.transform.parent.gameObject);
	}


	public void PlayerHit ()
	{
		player.Explode ();
		lives.Substract (1);
		if (lives.getLives () <= 0) {
			GameOver ();
			return;
		}
		ResetToCheckpoint ();
	}


	private IEnumerator MoveCameraToPlayer ()
	{
		Vector3 targetPos = new Vector3 (0, 0, score.GetScore () - 50);
		
		float scalar = 1;
		cameraInMotion = true;
		do {
			if (player)
				targetPos = PlayerPos + cameraOffset;
			cam.transform.position = Vector3.Lerp (cam.transform.position, targetPos, Time.deltaTime * scalar);
			scalar += Time.deltaTime;
			yield return null;
		} while ((cam.transform.position - (targetPos)).magnitude > 0.5f);
		cameraInMotion = false;

		if (player)
			player.ResetSpeed ();
	}


	private void ResetToCheckpoint ()
	{
		player.SetPosition (lastCheckpoint);
		player.Stop ();
		StartCoroutine (MoveCameraToPlayer ());
	}


	public void GameOver ()
	{
		gameOver = true;
		panel.FadeToBlack ();
		gameOverText.FadeInText (score.GetScore ());
		player.Die ();
	}


	public void Restart ()
	{
		SceneManager.LoadScene (0);
	}
}
