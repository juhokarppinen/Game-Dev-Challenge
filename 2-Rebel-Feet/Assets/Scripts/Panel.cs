using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Panel : MonoBehaviour
{
	private Image image;
	private float fadeDuration = 4f;

	void Start ()
	{
		image = GetComponent<Image> ();
	}


	public void FadeToBlack ()
	{
		StartCoroutine (Fade ());
	}


	private IEnumerator Fade ()
	{
		float r = image.color.r;
		float g = image.color.g;
		float b = image.color.b;
		float a = 0;
		do {
			image.color = new Color (r, g, b, a);
			a += Time.deltaTime / fadeDuration;
			yield return null;
		} while (a < 1);
	}
}
