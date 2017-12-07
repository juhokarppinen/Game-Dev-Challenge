using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour
{
	public float rotationSpeed;
	public float fallSpeed;


	/// <summary>
	/// Move and rotate the PowerUp.
	/// </summary>
	void Update ()
	{
		transform.rotation *= Quaternion.Euler (0, Time.deltaTime * rotationSpeed, 0);
		transform.position -= new Vector3 (0, 0, Time.deltaTime * fallSpeed);
	}


	void OnTriggerEnter (Collider other)
	{
		if (other.CompareTag ("Shredder")) {
			Destroy (gameObject);
		}
	}
}
