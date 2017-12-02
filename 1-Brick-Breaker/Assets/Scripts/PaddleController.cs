using UnityEngine;
using System.Collections;

public class PaddleController : MonoBehaviour
{
	public float keyboardSpeedScale;
	public float mouseSpeedScale;
	public bool autoPlay;
	public AudioClip paddleSmaller;


	private const float playAreaWidth = 14f;
	private new Renderer renderer;

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
		get { return renderer.bounds.extents.x; }
	}

	private float Left {
		get { return X - HalfSize; }
		set { transform.position = new Vector3 (value + HalfSize, Y, Z); }
	}

	private float Right {
		get { return X + HalfSize; }
		set { transform.position = new Vector3 (value - HalfSize, Y, Z); }
	}


	void Start ()
	{
		renderer = GetComponentInChildren<Renderer> ();
	}


	void Update ()
	{
		if (Input.GetKey (KeyCode.M) && Input.GetKey (KeyCode.I) && Input.GetKey (KeyCode.L))
			autoPlay = !autoPlay;	
	}


	void FixedUpdate ()
	{
		float mouseMovement = Input.GetAxisRaw ("Mouse X");
		float keyboardMovement = Input.GetAxisRaw ("Horizontal");
		float x = X + (keyboardMovement * keyboardSpeedScale + mouseMovement * mouseSpeedScale) * Time.deltaTime;

		if (autoPlay)
			x = GameManager.GetBallX ();

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
		if (IsSmall)
			return;
		
		gameObject.transform.localScale = new Vector3 (0.5f, 1f, 1f);
		AudioSource.PlayClipAtPoint (paddleSmaller, transform.position);
	}


	public void MakeNormal ()
	{
		gameObject.transform.localScale = new Vector3 (1f, 1f, 1f);
	}
}
