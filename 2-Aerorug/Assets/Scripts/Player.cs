using MidiJack;
using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	private GameManager gameManager;

	private Quaternion targetRot;
	private Quaternion rotNeutral;
	private Quaternion rotFront;
	private Quaternion rotBack;
	private Quaternion rotLeft;
	private Quaternion rotRight;
	private Quaternion rotFrontLeft;
	private Quaternion rotFrontRight;
	private Quaternion rotBackLeft;
	private Quaternion rotBackRight;

	private bool isJumping = false;
	private float jumpAmount;
	private float jumpSpeed;

	private float rotSpeed;
	private float speed;
	private float startSpeed;

	public float Speed {
		get { return speed; }
		set { speed = value; }
	}

	// Map the MIDI notes of the MIDI map to steering directions.
	private int LEFT = 64;
	private int RIGHT = 67;
	private int FRONT = 59;
	private int BACK = 62;
	private int LEFT_HARD = 72;
	private int RIGHT_HARD = 55;
	//	private int SHIELD_1 = 57;
	//	private int SHIELD_2 = 69;
	//	private int MISSILE_1 = 65;
	//	private int MISSILE_2 = 53;
	//	private int MISSILE_3 = 71;
	//	private int MISSILE_4 = 60;


	void Start ()
	{
		gameManager = GameManager.instance;
	
		float hardRot = 33f;
		float softRot = 22f;

		rotNeutral = Quaternion.identity;
		rotFront = Quaternion.Euler (hardRot, 0, 0);
		rotBack = Quaternion.Euler (-hardRot, 0, 0);
		rotLeft = Quaternion.Euler (0, -hardRot, hardRot);
		rotRight = Quaternion.Euler (0, hardRot, -hardRot);
		rotFrontLeft = Quaternion.Euler (softRot, -softRot, softRot);
		rotFrontRight = Quaternion.Euler (softRot, softRot, -softRot);
		rotBackLeft = Quaternion.Euler (-softRot, -softRot, softRot);
		rotBackRight = Quaternion.Euler (-softRot, softRot, -softRot);

		jumpAmount = 30f;
		jumpSpeed = 100f;
		rotSpeed = 120f;
		startSpeed = 20f;
		speed = startSpeed;
	}


	void Update ()
	{
		if (!isJumping)
			GetInput ();
		Rotate ();
		Move ();
	}

	
	void LateUpdate ()
	{
		gameManager.FollowCamera ();
	}


	private void GetInput ()
	{
		if (InputFrontLeft ())
			targetRot = rotFrontLeft;
		else if (InputFrontRight ())
			targetRot = rotFrontRight;
		else if (InputBackLeft ())
			targetRot = rotBackLeft;
		else if (InputBackRight ())
			targetRot = rotBackRight;
		else if (InputFront ())
			targetRot = rotFront;
		else if (InputRight ())
			targetRot = rotRight;
		else if (InputBack ())
			targetRot = rotBack;
		else if (InputLeft ())
			targetRot = rotLeft;
		else
			targetRot = rotNeutral;

		if (InputLeftHard ())
			PerformHardLeft ();
		if (InputRightHard ())
			PerformHardRight ();
	}


	private bool InputFrontLeft ()
	{
		return ((Key (FRONT) && Key (LEFT)) || (Input.GetKey (KeyCode.UpArrow) && Input.GetKey (KeyCode.LeftArrow)));
	}


	private bool InputFrontRight ()
	{
		return ((Key (FRONT) && Key (RIGHT)) || (Input.GetKey (KeyCode.UpArrow) && Input.GetKey (KeyCode.RightArrow)));
	}


	private bool InputBackLeft ()
	{
		return ((Key (BACK) && Key (LEFT)) || (Input.GetKey (KeyCode.DownArrow) && Input.GetKey (KeyCode.LeftArrow)));
	}


	private bool InputBackRight ()
	{
		return ((Key (BACK) && Key (RIGHT)) || (Input.GetKey (KeyCode.DownArrow) && Input.GetKey (KeyCode.RightArrow)));
	}


	private bool InputLeft ()
	{
		return (Key (LEFT) || Input.GetKey (KeyCode.LeftArrow));
	}


	private bool InputRight ()
	{
		return (Key (RIGHT) || Input.GetKey (KeyCode.RightArrow));
	}


	private bool InputFront ()
	{
		return (Key (FRONT) || Input.GetKey (KeyCode.UpArrow));
	}


	private bool InputBack ()
	{
		return (Key (BACK) || Input.GetKey (KeyCode.DownArrow));
	}


	private bool InputLeftHard ()
	{
		return (Key (LEFT_HARD) || Input.GetKey (KeyCode.A));
	}


	private bool InputRightHard ()
	{
		return (Key (RIGHT_HARD) || Input.GetKey (KeyCode.D));
	}


	private bool Key (int note)
	{
		return MidiMaster.GetKey (note) > 0;
	}


	private void Rotate ()
	{
		transform.rotation = Quaternion.RotateTowards (transform.rotation, targetRot, rotSpeed * Time.deltaTime);
	}


	private void Move ()
	{
		transform.position += transform.forward * Time.deltaTime * speed;
	}


	public void SetPosition (Vector3 pos)
	{
		transform.position = pos;
	}


	public void PerformHardLeft ()
	{
		if (!isJumping) {
			targetRot = rotLeft;
			StartCoroutine (LeftHard ());
		}
	}


	public void PerformHardRight ()
	{
		if (!isJumping) {
			targetRot = rotRight;
			StartCoroutine (RightHard ());
		}
	}


	public IEnumerator LeftHard ()
	{
		isJumping = true;
		float remaining = jumpAmount;

		while (remaining > float.Epsilon) {
			float xOffset = Time.deltaTime * jumpSpeed; 
			remaining -= xOffset;
			float x = transform.position.x - xOffset;
			SetPosition (new Vector3 (x, transform.position.y, transform.position.z));
			yield return null;
		}
		isJumping = false;
	}


	public IEnumerator RightHard ()
	{
		isJumping = true;
		float remaining = jumpAmount;

		while (remaining > float.Epsilon) {
			float xOffset = Time.deltaTime * jumpSpeed; 
			remaining -= xOffset;
			float x = transform.position.x + xOffset;
			SetPosition (new Vector3 (x, transform.position.y, transform.position.z));
			yield return null;
		}
		isJumping = false;
	}


	public void Die ()
	{
		Destroy (gameObject);
	}
}
