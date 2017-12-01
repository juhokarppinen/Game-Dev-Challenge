using UnityEngine;
using System.Collections;

public class BrickController : MonoBehaviour
{
	private static int count = 0;

	public int scoreValue;
	public GameObject explosion;

	private GameObject particleSystemParent;

	void Start ()
	{
		count++;

		particleSystemParent = GameObject.Find ("Particle Effects");
		if (!particleSystemParent) {
			particleSystemParent = new GameObject ("Particle Effects");
		}
	}


	void OnCollisionEnter (Collision collision)
	{
		GameObject newExplosion = (GameObject)Instantiate (explosion, transform.position, Quaternion.identity);
		newExplosion.transform.SetParent (particleSystemParent.transform);
		SoundManager.Play (SoundManager.Sound.Explosion, transform.position);
		GameManager.AddToScore (scoreValue);

		count--;
		if (count <= 0)
			GameManager.Win ();
			
		Destroy (gameObject);
	}
}
