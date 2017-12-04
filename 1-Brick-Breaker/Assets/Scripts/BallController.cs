using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour
{
	private const string LAUNCH_BUTTON = "Launch";
	private const float STATIONARY_LIMIT = 0.001f;
	private const float VELOCITY_COMPONENT_LIMIT = 3.0f;
	
	public float initialSpeed;
	public AudioClip ballBouncesFromBrick;
	public AudioClip ballBouncesFromPaddle;
	public AudioClip ballBouncesFromWall;
	public AudioClip ballLost;

	private bool shouldBeReplaced = true;
	private int speedLevel = 0;
	private int paddleHits = 0;
	private float defaultX;
	private float defaultY;
	private float defaultZ;
	private float[] speedLevels = { 10.0f, 12.0f, 14.0f, 16.0f, 18.0f };
	private Rigidbody rigidBody;


	public bool ShouldBeReplaced {
		get { return shouldBeReplaced; }
		set { shouldBeReplaced = value; }
	}


	void Start ()
	{
		defaultX = transform.position.x;
		defaultY = transform.position.y;
		defaultZ = transform.position.z;
		rigidBody = GetComponent<Rigidbody> ();
	}


	void Update ()
	{
		if (LaunchButtonPressed ())
			LaunchBall ();

		if (IsStationary ())
			FollowPaddle ();
	}


	private void LoseBall ()
	{
		GameManager.BallLost (transform.position);
		AudioSource.PlayClipAtPoint (ballLost, transform.position);
		speedLevel = 0;
		paddleHits = 0;
		ReplaceBall ();
	}


	private bool LaunchButtonPressed ()
	{
		return Input.GetButtonDown (LAUNCH_BUTTON);
	}


	private void ReplaceBall ()
	{
		if (ShouldBeReplaced) {
			rigidBody.velocity = Vector3.zero;
			transform.position = new Vector3 (defaultX, defaultY, defaultZ);
		} else {
			gameObject.SetActive (false);
		}
	}


	private void LaunchBall ()
	{
		if (!IsStationary ())
			return;
		
		Vector3 direction = new Vector3 (Random.value - .5f, 0, Random.value);
		direction.Normalize ();
		float x = direction.x * speedLevels [0];
		float z = direction.z * speedLevels [0];
		rigidBody.velocity = new Vector3 (x, 0, z);
	}


	private bool IsStationary ()
	{
		return rigidBody.velocity.magnitude < STATIONARY_LIMIT;
	}


	private void FollowPaddle ()
	{
		float x = GameManager.GetPaddleX ();
		transform.position = new Vector3 (x, defaultY, defaultZ);
	}


	/* The only triggers in the game are "shredders" which catch any falling balls */
	void OnTriggerEnter (Collider other)
	{
		LoseBall ();
	}


	void OnCollisionEnter (Collision collision)
	{
		Collider other = collision.collider;
		if (other.CompareTag ("Ground"))
			return;

		if (other.CompareTag ("Paddle")) {
			AudioSource.PlayClipAtPoint (ballBouncesFromPaddle, transform.position);
			paddleHits += 1;
			if (speedLevel < 1 && paddleHits >= 4) {
				speedLevel = 1;
			}
			if (speedLevel < 2 && paddleHits >= 12) {
				speedLevel = 2;
			}
		}

		if (other.CompareTag ("Wall")) {
			AudioSource.PlayClipAtPoint (ballBouncesFromWall, transform.position);
			if (other.gameObject.name == "Top Wall") {
				GameManager.BallHitTopWall ();
			}
		}

		if (other.CompareTag ("Brick")) {
			AudioSource.PlayClipAtPoint (ballBouncesFromBrick, transform.position);
			if (speedLevel < 3 && other.name == "Orange Brick") {
				speedLevel = 3;
			}
			if (speedLevel < 4 && other.name == "Red Brick") {
				speedLevel = 4;
			}
		}
			

		/* This prevents the ball from changing its speed erroneously when the
		 * physics engine glitches during collisions. */
		SetSpeed (speedLevel);

		/* Sometimes one of the velocity's components approaches zero, which often 
		 * makes the ball "stuck" on an axis. This method checks that this never happens. */
		MaintainDirection ();
	}


	private void SetSpeed (int level)
	{
		Vector3 direction = rigidBody.velocity.normalized;
		float magnitude = speedLevels [level];
		float x = direction.x * magnitude;
		float z = direction.z * magnitude;
		rigidBody.velocity = new Vector3 (x, 0f, z);
	}


	private void MaintainDirection ()
	{
		float x = rigidBody.velocity.x;
		float xMag = Mathf.Abs (x);
		float xSign = x >= 0f ? 1f : -1f;
		
		float z = rigidBody.velocity.z;
		float zMag = Mathf.Abs (z);
		float zSign = z >= 0f ? 1f : -1f;

		if (xMag < VELOCITY_COMPONENT_LIMIT) {
//			Debug.Log ("xMag:" + xMag + " zMag: " + zMag + " LIM: " + VELOCITY_COMPONENT_LIMIT);
			float newX = 2 * VELOCITY_COMPONENT_LIMIT * xSign;
			rigidBody.velocity = new Vector3 (newX, 0, z);
			SetSpeed (speedLevel);
		}

		if (zMag < VELOCITY_COMPONENT_LIMIT) {
//			Debug.Log ("xMag:" + xMag + " zMag: " + zMag + " LIM: " + VELOCITY_COMPONENT_LIMIT);
			float newZ = 2 * VELOCITY_COMPONENT_LIMIT * zSign;
			rigidBody.velocity = new Vector3 (x, 0, newZ);
			SetSpeed (speedLevel);			
		}
	}


	public void Explode ()
	{
		gameObject.SetActive (false);
	}
}
