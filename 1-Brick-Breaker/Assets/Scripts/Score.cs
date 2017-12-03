using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : IntegerAmount
{
	public Text scoreText;

	public override void Add (int amount)
	{
		base.Add (amount);
		scoreText.text = "SCORE: " + this.amount;
	}

}
