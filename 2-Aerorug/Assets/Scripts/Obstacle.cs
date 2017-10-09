using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour
{
	private GameManager gameManager;
	private Material material;
	private float fadeRate = .2f;

	void Start ()
	{
		gameManager = GameManager.instance;
		material = GetComponent<MeshRenderer> ().material;
	}


	void OnCollisionEnter (Collision collision)
	{
		if (collision.collider.tag == "Player") {
			gameManager.HitObstacle ();	
		}
	}

	void Update ()
	{
		if (Input.GetKey (KeyCode.U))
			MakeTransparent ();
	}

	public void MakeTransparent ()
	{
		StartCoroutine (FadeOut ());
	}


	private IEnumerator FadeOut ()
	{
		float r = material.color.r;
		float g = material.color.g;
		float b = material.color.b;
		while (material.color.a > 0) {
			float a = material.color.a - Time.deltaTime * fadeRate;
			material.color = new Color (r, g, b, a);
			yield return null;
		}
	}

}
