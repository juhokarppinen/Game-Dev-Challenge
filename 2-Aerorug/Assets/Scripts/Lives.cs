using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using System.Collections;

public class Lives : MonoBehaviour
{
	public Text livesText;

	private int lives = 3;

	void Awake ()
	{
		livesText = GetComponent<Text> ();
	}

	public int getLives ()
	{
		return lives;
	}

	public void Add (int amount)
	{
		Assert.IsTrue (amount >= 0);
		lives += amount;
		UpdateText ();
	}

	public void Substract (int amount)
	{
		Assert.IsTrue (amount >= 0);
		lives -= amount;
		UpdateText ();
	}

	private void UpdateText ()
	{
		livesText.text = lives.ToString ();
	}
}
