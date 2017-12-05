using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Takes care of tracking lives and updating the UI element which displays them.
/// </summary>
public class Lives : IntegerAmount
{
	private LivesText livesText;

	void Awake ()
	{
		livesText = FindObjectOfType<LivesText> ();
	}


	void Start ()
	{
		livesText.UpdateText (this.amount);
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


	/// <summary>
	/// Set lives to the specified amount and update the associated UI element.
	/// </summary>
	/// <param name="amount">Amount.</param>
	public override void Set (int amount)
	{
		base.Set (amount);
		livesText.UpdateText (this.amount);
	}
}
