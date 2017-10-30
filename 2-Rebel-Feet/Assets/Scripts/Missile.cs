using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour
{
	public float speed;
	public float trackingSpeed;

	private GameManager gameManager;
	private GameObject missileParent;

	
	void Start ()
	{
		gameManager = FindObjectOfType<GameManager> ();
		missileParent = GameObject.Find ("Missiles");
		transform.parent = missileParent.transform;
		GetComponent<AudioSource> ().Play ();
	}

	
	void Update ()
	{
		Vector3 direction = (gameManager.PlayerPos - transform.position).normalized;
		Quaternion targetRotation = Quaternion.LookRotation (direction);
		transform.Translate (Vector3.forward * speed * Time.deltaTime);
		transform.rotation = Quaternion.RotateTowards (
			transform.rotation, 
			targetRotation, 
			trackingSpeed * Time.deltaTime);
	}


	void OnCollisionEnter (Collision collision)
	{
		GetComponent<Explodable> ().Explode ();
	}
}
