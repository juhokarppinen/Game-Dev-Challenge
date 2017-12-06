using UnityEngine;

public class Explodable : MonoBehaviour
{
	public GameObject explosionEffect;
	public AudioClip explosionSound;

	public void Explode (Vector3 position)
	{
		if (explosionEffect) {
			GameObject newExplosion = (GameObject)Instantiate (explosionEffect, transform.position, Quaternion.identity);
			ParticleEffectContainer.Contain (newExplosion);
		}

		if (explosionSound) {
			AudioSource.PlayClipAtPoint (explosionSound, position);
		}
	}
}
