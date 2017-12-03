using UnityEngine;
using System.Collections;

public class LifeCounter : MonoBehaviour
{
	private float defaultY;
	private Rigidbody rigidBody;


	void Start ()
	{
		defaultY = transform.position.y;
		rigidBody = GetComponent<Rigidbody> ();
		rigidBody.useGravity = false;
	}


	void Update ()
	{
		if (IsBelowStartingLevel ()) {
			Dispose ();
		}
	}


	public void Launch ()
	{
		rigidBody.useGravity = true;
	}


	private bool IsBelowStartingLevel ()
	{
		return transform.position.y < defaultY;
	}


	public void Dispose ()
	{
		GetComponent<Explodable> ().Explode (transform.position);
		Destroy (gameObject);
	}
}
