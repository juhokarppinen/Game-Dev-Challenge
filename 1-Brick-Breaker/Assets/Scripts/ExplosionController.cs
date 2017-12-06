using UnityEngine;

public class ExplosionController : MonoBehaviour
{
	private ParticleSystem ps;


	void Start ()
	{
		ps = GetComponent<ParticleSystem> ();
	}


	void Update ()
	{
		if (!ps.IsAlive ())
			Destroy (gameObject);
	}
}
