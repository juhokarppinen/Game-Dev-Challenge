using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyShip : MonoBehaviour
{
	public GameObject missile;
	public float speed;
	public float attackDistance = 150;
	public float trackingSpeed = 20;
	private GameManager gameManager;
	private GameObject enemyParent;
	private bool hasMissile = true;
	private Quaternion targetRotation;


	void Start ()
	{
		gameManager = FindObjectOfType<GameManager> ();
		enemyParent = GameObject.Find ("Enemies");
		transform.parent = enemyParent.transform;
	}


	void Update ()
	{
		if (IsCloseEnough ()) {
			TrackPlayer ();
			if (hasMissile)
				ShootMissile ();
		}
		transform.Translate (Vector3.forward * speed * Time.deltaTime);
	}


	private bool IsCloseEnough ()
	{
		return transform.position.z - gameManager.PlayerPos.z < attackDistance;
	}


	private void TrackPlayer ()
	{
		Vector3 direction = (gameManager.PlayerPos - transform.position).normalized;
		Quaternion targetRotation = Quaternion.LookRotation (direction);
		transform.Translate (Vector3.forward * speed * Time.deltaTime);
		transform.rotation = Quaternion.RotateTowards (
			transform.rotation, 
			targetRotation, 
			trackingSpeed * Time.deltaTime);
	}


	public void ShootMissile ()
	{
		hasMissile = false;
		GameObject newMissile = (GameObject)Instantiate (
			                        missile, 
			                        transform.position,
			                        Quaternion.identity);
		newMissile.transform.forward = transform.forward; // new Vector3 (0, 0, -1);
	}


	void OnCollisionEnter (Collision collision)
	{
		GetComponent<Explodable> ().Explode ();
	}


	public void SetDirection (Vector3 targetPosition)
	{
		Vector3 direction = (targetPosition - transform.position).normalized;
		transform.rotation = Quaternion.LookRotation (direction);
	}
}
