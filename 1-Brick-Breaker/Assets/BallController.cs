using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour
{
	public float fallDepth;
	public float startSpeed;

	private float defaultX;
	private float defaultY;
	private float defaultZ;

	private Rigidbody rigidBody;
	private PaddleController paddle;

	// Use this for initialization
	void Start ()
	{
		defaultX = transform.position.x;
		defaultY = transform.position.y;
		defaultZ = transform.position.z;
		rigidBody = GetComponent<Rigidbody> ();
		paddle = FindObjectOfType<PaddleController> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetButtonDown ("Fire1")) {
			Shoot ();
		}
		if (HasFallen ()) {
			Replace ();
		}
		if (rigidBody.velocity.magnitude < 0.001f) {
			transform.position = new Vector3 (paddle.X, defaultY, defaultZ);
		}
	}


	void FixedUpdate ()
	{
	}

	private void Replace ()
	{
		rigidBody.velocity = Vector3.zero;
		transform.position = new Vector3 (defaultX, defaultY, defaultZ);
	}

	private void Shoot ()
	{
		if (rigidBody.velocity.magnitude > 0)
			return;
		Vector3 force = new Vector3 (Random.value - .5f, 0, Random.value);
		force.Normalize ();
		rigidBody.AddForce (force * startSpeed);
	}

	private bool HasFallen ()
	{
		return transform.position.y <= fallDepth;
	}
}
