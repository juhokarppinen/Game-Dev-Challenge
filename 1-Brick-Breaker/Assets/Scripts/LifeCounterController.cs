using UnityEngine;
using System.Collections;

public class LifeCounterController : MonoBehaviour
{
	public GameObject explosion;

	private float defaultY;
	private Rigidbody rigidBody;
	private GameObject particleEffectContainer;

	void Start ()
	{
		defaultY = transform.position.y;
		rigidBody = GetComponent<Rigidbody> ();
		rigidBody.useGravity = false;
		particleEffectContainer = GameObject.Find ("Particle Effects");
		if (!particleEffectContainer) {
			particleEffectContainer = new GameObject ("Particle Effects");
		}
	}
	
	// Update is called once per frame
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
		GameObject newExplosion = (GameObject)Instantiate (explosion, transform.position, Quaternion.identity);
		newExplosion.transform.parent = particleEffectContainer.transform;
		Destroy (gameObject);
	}
}
