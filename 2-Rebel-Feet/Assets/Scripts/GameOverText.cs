using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverText : MonoBehaviour
{
	private Text text;
	private RestartText restartText;
	private GameManager gameManager;
	private float fadeDuration = 4f;

	void Start ()
	{
		text = GetComponent<Text> ();
		gameManager = FindObjectOfType<GameManager> ();
		restartText = FindObjectOfType<RestartText> ();
	}


	public void FadeInText (float finalScore)
	{
		text.text = "Your team survived for " + Mathf.Ceil (finalScore).ToString () + " meters...";
		StartCoroutine (FadeIn ());
	}


	private IEnumerator FadeIn ()
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

		StartCoroutine (restartText.FadeIn ());
		gameManager.EndScreenLoaded = true;
	}
}
