using UnityEngine;
using System.Collections;

public class Explodable : MonoBehaviour
{
	public GameObject explosion;
	public AudioClip[] sounds;
	private GameObject explosionParent;

	void Start ()
	{
		explosionParent = GameObject.Find ("Effects");
	}

	
	public void Explode ()
	{
		GameObject newExplosion = (GameObject)Instantiate (explosion, gameObject.transform.position, Quaternion.identity);
		newExplosion.GetComponent<GarbageCollector> ().DestroyIn (5);
		newExplosion.transform.parent = explosionParent.transform;
		AudioSource.PlayClipAtPoint (sounds [Random.Range (0, sounds.Length - 1)], transform.position);
		Destroy (gameObject);
	}
}
