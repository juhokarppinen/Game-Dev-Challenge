using UnityEngine;
using System.Collections;

public class LifeCounter : MonoBehaviour
{
	public void Dispose ()
	{
		GetComponent<Explodable> ().Explode (transform.position);
		Destroy (gameObject);
	}
}
