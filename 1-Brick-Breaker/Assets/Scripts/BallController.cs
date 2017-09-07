using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour
{
	public float ballLostY = -1;
	public float ballLostZ = -10;
	public float launchVelocity = 1000;
	public AudioClip wallBounceSound;
	public AudioClip paddleBounceSound;
	public AudioClip brickBounceSound;
	public AudioClip ballLostSound;

	private const string LAUNCH_BUTTON = "Jump";
	private const float STATIONARY_LIMIT = 0.001f;

	private bool shouldBeReplaced = true;
	private float defaultX;
	private float defaultY;
	private float defaultZ;
	private Rigidbody rigidBody;
	private PaddleController paddle;
	private AudioClip collisionClip;
	private LivesController lives;

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
		paddle = FindObjectOfType<PaddleController> ();
		lives = FindObjectOfType<LivesController> ();
	}

	void Update ()
	{
		if (LaunchButtonPressed ()) {
			LaunchBall ();
		}
		if (HasFallen ()) {
			lives.Decrease ();
			AudioSource.PlayClipAtPoint (ballLostSound, transform.position);
			ReplaceBall ();
		}
		if (IsStationary ()) {
			FollowPaddle ();
		}
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
		if (!IsStationary ()) {
			return;
		}
		Vector3 force = new Vector3 (Random.value - .5f, 0, Random.value);
		force.Normalize ();
		rigidBody.AddForce (force * launchVelocity);
	}

	private bool HasFallen ()
	{
		return transform.position.y <= ballLostY /*|| transform.position.z <= ballLostZ*/;
	}

	private bool IsStationary ()
	{
		return rigidBody.velocity.magnitude < STATIONARY_LIMIT;
	}

	private void FollowPaddle ()
	{
		transform.position = new Vector3 (paddle.X, defaultY, defaultZ);
	}

	void OnCollisionEnter (Collision collision)
	{
		Collider other = collision.collider;
		collisionClip = null;
		if (other.CompareTag ("Paddle"))
			collisionClip = paddleBounceSound;
		if (other.CompareTag ("Wall"))
			collisionClip = wallBounceSound;
		if (other.CompareTag ("Brick"))
			collisionClip = brickBounceSound;
		if (collisionClip)
			AudioSource.PlayClipAtPoint (collisionClip, transform.position);
	}
}
