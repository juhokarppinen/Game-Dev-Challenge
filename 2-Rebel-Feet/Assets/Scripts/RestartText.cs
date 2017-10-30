using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RestartText : MonoBehaviour
{
	private Text text;
	private float fadeDuration = 4f;


	void Start ()
	{
		text = GetComponent<Text> ();
	}


	public IEnumerator FadeIn ()
	{
		float r = 1;
		float g = 1;
		float b = 1;
		float a = 0;
		do {
			text.color = new Color (r, g, b, a);
			a += Time.deltaTime / fadeDuration;
			yield return null;
		} while (a < 1);
	}
}
