using UnityEngine;

public class PaddleController : GameManagerCollaborator
{
	public float keyboardSpeedScale;
	public float mouseSpeedScale;
	public bool autoPlay;
	public GameObject ball;
	public AudioClip paddleSmaller;
	public AudioClip paddleBigger;

	private const float playAreaWidth = 14f;

	private Renderer paddleRenderer;
	private InputManager input;
	private float smallTimer = 0;

	public bool IsSmall {
		get { return gameObject.transform.localScale.x == 0.5f; }
	}

	public float X {
		get { return transform.position.x; }
	}

	private float Y {
		get { return transform.position.y; }
	}

	private float Z {
		get { return transform.position.z; }
	}

	private float HalfSize {
		get { return paddleRenderer.bounds.extents.x; }
	}

	private float Left {
		get { return X - HalfSize; }
		set { transform.position = new Vector3 (value + HalfSize, Y, Z); }
	}

	private float Right {
		get { return X + HalfSize; }
		set { transform.position = new Vector3 (value - HalfSize, Y, Z); }
	}


	void Awake ()
	{
		input = GetComponent<InputManager> ();
		paddleRenderer = GetComponentInChildren<Renderer> ();
	}


	void Update ()
	{
		if (input.LaunchButtonDown)
			LaunchBall ();
		if (smallTimer > 0) {
			smallTimer -= Time.deltaTime;
		} else if (IsSmall) {
			MakeNormal ();
		}
	}


	void FixedUpdate ()
	{
		float mouseMovement = input.MouseAxisX;
		float keyboardMovement = input.ControllerAxisX;
		float x = X + (keyboardMovement * keyboardSpeedScale + mouseMovement * mouseSpeedScale) * Time.deltaTime;

		transform.position = new Vector3 (x, Y, Z);

		if (Left < -playAreaWidth)
			Left = -playAreaWidth;
		if (Right > playAreaWidth)
			Right = playAreaWidth;

		if (Input.GetKeyDown (KeyCode.B))
			MakeSmaller ();
	}


	public void Explode ()
	{
		GetComponent<Explodable> ().Explode (transform.position);
		gameObject.SetActive (false);
	}


	public void MakeSmaller ()
	{
		smallTimer += 10;

		if (IsSmall)
			return;
		
		gameObject.transform.localScale = new Vector3 (0.5f, 1f, 1f);
		AudioSource.PlayClipAtPoint (paddleSmaller, transform.position);
	}


	public void MakeNormal ()
	{
		gameObject.transform.localScale = new Vector3 (1f, 1f, 1f);
		AudioSource.PlayClipAtPoint (paddleBigger, transform.position);
	}


	private void LaunchBall ()
	{
		if (gameManager.HasLives ()) {
			gameManager.DecrementLives ();
			GameObject newBall = (GameObject)Instantiate (ball);
			newBall.transform.position = transform.position + new Vector3 (0, 0, 1);

			Vector3 direction = new Vector3 (Random.value - .5f, 0, Random.value);
			direction.Normalize ();
			float x = direction.x * 10;
			float z = direction.z * 10;
			newBall.GetComponent<Rigidbody> ().velocity = new Vector3 (x, 0, z);
		}
	}


	/// <summary>
	/// Pick up powerups.
	/// </summary>
	/// <param name="other">Other.</param>
	void OnTriggerEnter (Collider other)
	{
		if (other.CompareTag ("PowerUp")) {
			Destroy (other.gameObject);
			gameManager.GotPowerUp ();
		} else if (other.CompareTag ("PowerDown")) {
			Destroy (other.gameObject);
			MakeSmaller ();
		}
	}
}
