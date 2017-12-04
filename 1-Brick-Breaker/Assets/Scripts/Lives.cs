using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Takes care of tracking lives and updating the UI element which displays them. Currently the
/// amount of lives is hardcoded to 3.
/// </summary>
public class Lives : IntegerAmount
{
	private int lives = 3;
	private LivesText livesText;

	void Start ()
	{
		amount = lives;
		livesText = FindObjectOfType<LivesText> ();
	}


	/// <summary>
	/// Remove the specified amount of lives and update the UI element accordingly.
	/// </summary>
	/// <param name="amount">Amount.</param>
	public override void Remove (int amount)
	{
		base.Remove (amount);
		livesText.UpdateText (this.amount);
	}


	/// <summary>
	/// Add the specified amount of lives and update the UI element accordingly.
	/// </summary>
	/// <param name="amount">Amount.</param>
	public override void Add (int amount)
	{
		base.Add (amount);
		livesText.UpdateText (this.amount);
	}
}
