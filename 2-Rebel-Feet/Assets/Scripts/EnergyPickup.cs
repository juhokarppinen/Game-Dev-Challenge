using UnityEngine;
using System.Collections;

public class EnergyPickup : MonoBehaviour
{
	public float energyAmount = 2f;
	public GameObject pop;
	public GameObject popParent;
	public AudioClip pickupSound;

	void OnCollisionEnter (Collision collision)
	{
		AudioSource.PlayClipAtPoint (pickupSound, transform.position, .5f);
		GameObject newPop = (GameObject)Instantiate (pop, transform.position, Quaternion.identity);
		newPop.GetComponent<GarbageCollector> ().DestroyIn (1);
		newPop.transform.parent = GameObject.Find ("Effects").transform;
		Destroy (gameObject);
	}
}
