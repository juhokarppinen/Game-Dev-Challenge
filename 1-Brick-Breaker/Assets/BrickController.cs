using UnityEngine;
using System.Collections;

public class BrickController : MonoBehaviour
{
	public GameObject explosion;
	private GameObject particleSystemParent;
	// Use this for initialization
	void Start ()
	{
		particleSystemParent = GameObject.Find ("Particle Effects");
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnCollisionEnter (Collision collision)
	{
		GameObject newExplosion = (GameObject)Instantiate (explosion, transform.position, Quaternion.identity);
		newExplosion.transform.SetParent (particleSystemParent.transform);
		Destroy (gameObject);
	}
}
