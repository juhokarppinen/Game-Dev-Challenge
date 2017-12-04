using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : IntegerAmount
{
	private ScoreText score;

	void Start ()
	{
		score = FindObjectOfType<ScoreText> ();
	}


	public override void Add (int amount)
	{
		base.Add (amount);
		score.UpdateText (this.amount);
	}

}
