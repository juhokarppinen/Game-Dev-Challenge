using UnityEngine;
using System.Collections;

public class BrickController : MonoBehaviour
{
	private static int count = 0;

	public int scoreValue;
	public GameObject explosion;
	public AudioClip explosionSound;

	private GameObject particleSystemParent;
	private ScoreController score;

	void Start ()
	{
		count++;

		particleSystemParent = GameObject.Find ("Particle Effects");
		if (!particleSystemParent) {
			particleSystemParent = new GameObject ("Particle Effects");
		}
		score = FindObjectOfType<ScoreController> ();
	}


	void OnCollisionEnter (Collision collision)
	{
		GameObject newExplosion = (GameObject)Instantiate (explosion, transform.position, Quaternion.identity);
		newExplosion.transform.SetParent (particleSystemParent.transform);
		AudioSource.PlayClipAtPoint (explosionSound, transform.position);
		score.AddPoints (scoreValue);
		count--;
		if (count <= 0) {
			score.Win ();
		}
			
		Destroy (gameObject);
	}
}
