using MidiJack;
using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	public GameObject explosion;
	public AudioClip[] explosionSounds;
	public float shieldCost = 15f;
	public float boostCost = 10f;

	private Thruster thruster;
	private MusicMatInput input;
	private GameObject explosionParent;
	private GameManager gameManager;
	private Shield shield;
	private Energy energy;

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
	private Quaternion rotBankLeft;
	private Quaternion rotBankRight;

	private bool shieldUp = false;
	private bool thrusterOn = false;

	private float rotSpeed = 120f;
	private float startSpeed = 20f;
	private float speed;
	private float speedIncrease = .75f;
	private float maxSpeed;
	private float xBoost = 0f;
	private float yBoost = 0f;
	private float boostDelta = 25f;
	private float maxBoost = 100f;

	public float Speed {
		get { return speed; }
		set { speed = value; }
	}


	void Start ()
	{
		thruster = FindObjectOfType<Thruster> ();
		input = GetComponent<MusicMatInput> ();
		shield = GetComponentInChildren<Shield> ();
		energy = FindObjectOfType<Energy> ();
		explosionParent = GameObject.Find ("Effects");
		gameManager = FindObjectOfType<GameManager> ();
	
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
		rotBankLeft = Quaternion.Euler (0, 0, hardRot);
		rotBankRight = Quaternion.Euler (0, 0, -hardRot);

		speed = startSpeed;
	}


	void Update ()
	{
		GetInput ();
		Rotate ();
		Move ();
		CheckShield ();
		CheckThruster ();
		UpdateSpeed ();
	}


	private void GetInput ()
	{
		if (input.InputFrontLeft ())
			targetRot = rotFrontLeft;
		else if (input.InputFrontRight ())
			targetRot = rotFrontRight;
		else if (input.InputBackLeft ())
			targetRot = rotBackLeft;
		else if (input.InputBackRight ())
			targetRot = rotBackRight;
		else if (input.InputFront ())
			targetRot = rotFront;
		else if (input.InputRight ())
			targetRot = rotRight;
		else if (input.InputBack ())
			targetRot = rotBack;
		else if (input.InputLeft ())
			targetRot = rotLeft;
		else
			targetRot = rotNeutral;

		if (input.InputLeftHard ())
			PerformHardLeft ();
		if (input.InputRightHard ())
			PerformHardRight ();
		if (input.InputFrontHard ())
			PerformHardFront ();
		if (input.InputBackHard ())
			PerformHardBack ();
		
		shieldUp = input.InputShield ();
		thrusterOn = (
		    input.InputBackHard () ||
		    input.InputFrontHard () ||
		    input.InputRightHard () ||
		    input.InputLeftHard ());
	}


	private void Rotate ()
	{
		transform.rotation = Quaternion.RotateTowards (transform.rotation, targetRot, rotSpeed * Time.deltaTime);
	}


	private void Move ()
	{
		transform.position += transform.forward * Time.deltaTime * speed;
		transform.position = new Vector3 (
			transform.position.x + xBoost,
			transform.position.y + yBoost,
			transform.position.z);
	}


	private void CheckShield ()
	{
		if (shieldUp && energy.Consume (shieldCost * Time.deltaTime)) {
			RaiseShield ();
		} else {
			LowerShield ();
		}
	}


	private void CheckThruster ()
	{
		if (thrusterOn && energy.Peek (boostCost * Time.deltaTime))
			thruster.StartThrust ();
		else
			thruster.StopThrust ();
	}


	private void UpdateSpeed ()
	{
		speed += Time.deltaTime * speedIncrease;
		maxSpeed = speed > maxSpeed ? speed : maxSpeed;
//		speed = speed > maxSpeed ? maxSpeed : speed;
		xBoost = Mathf.Lerp (xBoost, 0, Time.deltaTime * boostDelta / 4);
		yBoost = Mathf.Lerp (yBoost, 0, Time.deltaTime * boostDelta / 4);
	}


	public void SetPosition (Vector3 pos)
	{
		transform.position = pos;
	}


	public void Stop ()
	{
		speed = 0;
		xBoost = 0;
		yBoost = 0;
	}


	public void ResetSpeed ()
	{
		speed = 0.75f * maxSpeed;
		if (speed < startSpeed)
			speed = startSpeed;
		maxSpeed = 0;
		xBoost = 0;
		yBoost = 0;
	}


	public void PerformHardLeft ()
	{
		if (energy.Consume (boostCost * Time.deltaTime)) {
			targetRot = CombineRotations (targetRot, rotBankLeft);
			xBoost = Mathf.Lerp (xBoost, -maxBoost, Time.deltaTime / boostDelta * 2);
		} 
	}


	public void PerformHardRight ()
	{
		if (energy.Consume (boostCost * Time.deltaTime)) {
			targetRot = CombineRotations (targetRot, rotBankRight);
			xBoost = Mathf.Lerp (xBoost, maxBoost, Time.deltaTime / boostDelta * 2);
		} 
	}


	public void PerformHardBack ()
	{
		if (energy.Consume (boostCost * Time.deltaTime)) {
			targetRot = CombineRotations (targetRot, rotBack);
			yBoost = Mathf.Lerp (yBoost, maxBoost, Time.deltaTime / boostDelta * 2);
		} 
	}


	public void PerformHardFront ()
	{
		if (energy.Consume (boostCost * Time.deltaTime)) {
			targetRot = CombineRotations (targetRot, rotFront);
			yBoost = Mathf.Lerp (yBoost, -maxBoost, Time.deltaTime / boostDelta * 2);
		} 
	}


	private void RaiseShield ()
	{
		shieldUp = true;
		shield.Raise ();
	}


	private void LowerShield ()
	{
		shieldUp = false;
		shield.Lower ();
	}


	void OnCollisionEnter (Collision collision)
	{
		GameObject other = collision.gameObject;
		if (shieldUp && (other.tag == "Missile" || other.tag == "Enemy Ship")) {
			// Do Nothing
		} else if (other.tag == "Energy Pickup") {
			energy.Restore (other.GetComponent<EnergyPickup> ().energyAmount);
		} else
			gameManager.PlayerHit ();
	}


	public void Explode ()
	{
		GameObject newExplosion = (GameObject)Instantiate (explosion, transform.position, Quaternion.identity);
		newExplosion.transform.parent = explosionParent.transform;
		newExplosion.GetComponent<GarbageCollector> ().DestroyIn (5);
		AudioSource.PlayClipAtPoint (explosionSounds [Random.Range (0, explosionSounds.Length - 1)], transform.position);
	}


	public void Die ()
	{
		Destroy (gameObject);
	}


	private Quaternion CombineRotations (Quaternion a, Quaternion b)
	{
		float x = a.eulerAngles.x + b.eulerAngles.x;
		float y = a.eulerAngles.y + b.eulerAngles.y;
		float z = a.eulerAngles.z + b.eulerAngles.z;
		return Quaternion.Euler (x, y, z);
	}
}
