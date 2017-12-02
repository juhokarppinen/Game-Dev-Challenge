using UnityEngine;
using System.Collections;

public class BrickController : MonoBehaviour
{
	private static int count = 0;

	public int scoreValue;


	void Start ()
	{
		count++;
	}


	void OnCollisionEnter (Collision collision)
	{
		GameManager.AddToScore (scoreValue);

		count--;
		if (count <= 0)
			GameManager.Win ();
			
		GetComponent<Explodable> ().Explode (transform.position);
		Destroy (gameObject);
	}
}
